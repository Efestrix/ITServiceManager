using ITServiceManager.API.Dtos.Authentication;
using ITServiceManager.API.Dtos.Customer;
using ITServiceManager.API.Dtos.User;
using ITServiceManager.API.Entities;

namespace ITServiceManager.API.Mappings
{
    public static class UserMapping
    {
        public static UserDto ToDto(UserEntity entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Username = entity.Username,
                PasswordHash = entity.PasswordHash,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Role = entity.Role.ToString()
            };
        }

        public static UserEntity ToEntity(CreateUserDto dto)
        {
            return new UserEntity
            {
                Username = dto.Username,
                PasswordHash = dto.Password, // hash
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = Enum.Parse<UserRole>(dto.Role)
            };
        }
        public static UserEntity ToEntity(RegisterDto dto, string passwordHash)
        {
            return new UserEntity
            {
                PasswordHash = passwordHash
            };
        }

        public static void UpdateEntity(UserEntity entity, UpdateUserDto dto)
        {
            entity.Username = dto.Username;
            entity.PasswordHash = dto.Password; // hash
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.Role = Enum.Parse<UserRole>(dto.Role);
        }
    }
}
