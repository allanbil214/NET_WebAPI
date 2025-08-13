using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Services.Interfaces
{
    public interface IYoutuberService
    {
        Task<List<YoutuberReadDTO>> GetAllYoutubersAsync();
        Task<YoutuberReadDTO?> GetYoutuberByIdAsync(int id);
        Task<YoutuberReadDTO> CreateYoutuberAsync(YoutuberCreateDTO youtuberCreateDTO);
        Task<bool> UpdateYoutuberAsync(int id, YoutuberUpdateDTO youtuberUpdateDTO);
        Task<bool> DeleteYoutuberAsync(int id, YoutuberDeleteDTO youtuberDeleteDTO);
        Task<bool> YoutuberExistsAsync(int id);
        Task<bool> CanDeleteYoutuberAsync(int id);

        Task<List<YoutuberAdminReadDTO>> GetAllYoutubersAdminAsync();
        Task<YoutuberAdminReadDTO?> GetYoutuberByIdAdminAsync(int id);
        Task<bool> RestoreYoutuberAsync(int id);
        Task<bool> HardDeleteYoutuberAsync(int id);
    }
}