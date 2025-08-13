using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> RegisterAsync(UserRegisterDTO registerDto);
        Task<LoginResponseDTO> LoginAsync(UserLoginDTO loginDto);
    }
}