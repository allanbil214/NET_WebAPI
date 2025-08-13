using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        Task<List<Video>> GetAllAsync();
        Task<Video> GetByIdAsync(int Id);
        Task<Video> AddAsync(Video video);
        Task EditAsync(Video video);
        Task DeleteAsync(int Id);
        Task<List<Video?>> GetVideoByYoutuberAsync(int Id);
        Task<bool> VideoExistsAsync(int Id);

        Task<List<Video>> GetAllIncludingDeletedAsync();
        Task<Video> GetByIdIncludingDeletedAsync(int Id);
        Task RestoreAsync(int Id);
        Task HardDeleteAsync(int Id);
    }
}