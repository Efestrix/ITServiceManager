using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.User;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Middlewares;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DatabaseContext context)
            : base(context)
        {
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            List<UserEntity> users = await _context.Users.ToListAsync();

            return users
                .Select(UserMapping.ToDto)
                .ToList();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            UserEntity? user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new NotFoundException($"User with id {id} was not found");

            return UserMapping.ToDto(user);
        }
        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                throw new ValidationException("Username already exists.");

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new ValidationException("Email already exists.");

            ValidationResult validation = UserValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ValidationException(string.Join(Environment.NewLine, validation.Errors));

            UserEntity user = UserMapping.ToEntity(dto);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return UserMapping.ToDto(user);
        }

        public async Task DeleteAsync(int id)
        {
            UserEntity? user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new NotFoundException($"User with id {id} was not found.");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            ValidationResult validation = UserValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ValidationException(string.Join(Environment.NewLine, validation.Errors));

            UserEntity? user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new NotFoundException($"User with id {id} was not found.");

            UserMapping.UpdateEntity(user, dto);

            await _context.SaveChangesAsync();
        }
    }
}
