using ITServiceManager.API.Dtos.Authentication;

namespace ITServiceManager.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto dto);

        Task RegisterAsync(RegisterDto dto);
    }
}
