using ITServiceManager.API.Data;
using ITServiceManager.API.Dtos.User;
using ITServiceManager.API.Entities;
using ITServiceManager.API.Mappings;
using ITServiceManager.API.Services.Interfaces;
using ITServiceManager.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace ITServiceManager.API.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
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
            bool exists = await _context.Users.AnyAsync(u => u.Id == id);

            if (!exists)
                return null;

            UserEntity? user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            return UserMapping.ToDto(user);
        }
        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            ValidationResult validation = UserValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            UserEntity user = UserMapping.ToEntity(dto);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return UserMapping.ToDto(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool exists = await _context.Users.AnyAsync(u => u.Id == id);

            if (!exists)
                return false;

            UserEntity? user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            ValidationResult validation = UserValidator.Validate(dto);

            if (!validation.IsValid)
                throw new ArgumentException(string.Join(Environment.NewLine, validation.Errors));

            bool exists = await _context.Users.AnyAsync(u => u.Id == id);

            if (!exists)
                return false;

            UserEntity? user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            UserMapping.UpdateEntity(user, dto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
