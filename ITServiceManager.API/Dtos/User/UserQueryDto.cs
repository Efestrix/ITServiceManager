using ITServiceManager.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace ITServiceManager.API.Dtos.User
{
    public class UserQueryDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
    }
}
