using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.Authentication;
using ITServiceManager.API.Dtos.User;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITServiceManager.API.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            DatabaseContext context, 
            IConfiguration config,
            ILogger<AuthService> logger)
            : base(context)
        {
            _config = config;
            _logger = logger;
        }
        public async Task RegisterAsync(RegisterDto dto)
        {
            ValidationResult validation = AuthValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ValidationException(string.Join(Environment.NewLine, validation.Errors));

            if (await _context.Users.AnyAsync(x => x.Username == dto.Username))
                throw new ValidationException("Username already exists.");

            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
                throw new ValidationException("Email already exists.");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            UserEntity user = UserMapping.ToEntity(dto, passwordHash);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }
        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            ValidationResult validation = AuthValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ValidationException(string.Join(Environment.NewLine, validation.Errors));

            UserEntity? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
            {
                _logger.LogWarning(
                    "Failed login attempt for username {Username}.",
                    dto.Username);

                throw new UnauthorizedException("Invalid username");
            }
                

            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!passwordValid)
            {
                _logger.LogWarning(
                    "Failed login attempt for username {Username}.",
                    dto.Username);

                throw new UnauthorizedException("Invalid password");
            }
                

            _logger.LogInformation(
                "User {Username} logged in successfully.",
                user.Username);

            string token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Username = user.Username,
                Role = user.Role.ToString(),
                Expiration = DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_config["Jwt:ExpireMinutes"]))
            };
        }
        private string GenerateJwtToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            SigningCredentials credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);    
        }

        public async Task<UserDto> GetMeAsync(int userId)
        {
            UserEntity? user = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found.");

            return UserMapping.ToDto(user);
        }
    }
}
