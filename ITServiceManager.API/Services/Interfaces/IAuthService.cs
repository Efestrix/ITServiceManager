using ITServiceManager.API.Dtos.Authentication;
using ITServiceManager.API.Dtos.User;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto dto);

        Task RegisterAsync(RegisterDto dto);

        Task<UserDto> GetMeAsync(int userId);
    }
}
