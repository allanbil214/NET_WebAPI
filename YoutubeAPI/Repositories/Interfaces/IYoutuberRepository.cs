using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Repositories.Interfaces
{
    public interface IYoutuberRepository
    {
        Task<List<Youtuber>> GetAllAsync();
        Task<Youtuber> GetByIdAsync(int Id);
        Task<Youtuber> AddAsync(Youtuber Youtuber);
        Task EditAsync(Youtuber youtuber);
        Task DeleteAsync(int Id);
        Task<bool> YoutuberExistsAsync(int Id);

        Task<List<Youtuber>> GetAllIncludingDeletedAsync();
        Task<Youtuber> GetByIdIncludingDeletedAsync(int Id);
        Task RestoreAsync(int Id);
        Task HardDeleteAsync(int Id);
    }
}