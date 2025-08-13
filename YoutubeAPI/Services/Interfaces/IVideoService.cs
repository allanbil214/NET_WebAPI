using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Services.Interfaces
{
    public interface IVideoService
    {
        Task<List<VideoReadDTO>> GetAllVideosAsync();
        Task<VideoReadDTO?> GetVideoByIdAsync(int id);
        Task<VideoReadDTO> CreateVideoAsync(VideoCreateDTO videoCreateDTO);
        Task<bool> UpdateVideoAsync(int id, VideoUpdateDTO videoUpdateDTO);
        Task<bool> DeleteVideoAsync(int id, VideoDeleteDTO videoDeleteDTO);
        Task<List<VideoReadDTO>> GetVideosByYoutuberAsync(int youtuberId);
        Task<bool> VideoExistsAsync(int id);

        Task<List<VideoAdminReadDTO>> GetAllVideosAdminAsync();
        Task<VideoAdminReadDTO?> GetVideoByIdAdminAsync(int id);
        Task<bool> RestoreVideoAsync(int id);
        Task<bool> HardDeleteVideoAsync(int id);
    }
}