using Microsoft.EntityFrameworkCore;
using YoutubeAPI.Repositories.Interfaces;
using YoutubeAPI.Data;
using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Repositories.Implementations
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;
        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Video>> GetAllAsync()
        {
            var result = await _context.Videos
                            .Include(v => v.Youtuber)
                            .Where(v => v.IsDeleted == false)
                            .ToListAsync();
            return result;
        }

        public async Task<Video> GetByIdAsync(int Id)
        {
            var result = await _context.Videos
                            .Include(v => v.Youtuber)
                            .Where(v => v.Id == Id)
                            .Where(v => v.IsDeleted == false)
                            .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Video?>> GetVideoByYoutuberAsync(int YoutuberId)
        {
            var result = await _context.Videos
                            .Include(v => v.Youtuber)
                            .Where(v => v.YoutuberID == YoutuberId)
                            .Where(v => v.IsDeleted == false)
                            .ToListAsync();
            return result;
        }

        public async Task<Video> AddAsync(Video video)
        {
            video.CreatedAt = DateTime.UtcNow;
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(video.Id);
        }

        public async Task DeleteAsync(int Id)
        {
            var category = await _context.Videos.FirstOrDefaultAsync(v => v.Id == Id);
            if (category != null)
            {
                category.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

        }

        public async Task EditAsync(Video video)
        {
            video.UpdatedAt = DateTime.UtcNow;
            _context.Entry(video).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<bool> VideoExistsAsync(int Id)
        {
            var result = await _context.Videos.AnyAsync(v => v.Id == Id);
            return result;
        }

        public async Task<List<Video>> GetAllIncludingDeletedAsync()
        {
            return await _context.Videos
                            .Include(v => v.Youtuber)
                            .ToListAsync();
        }

        public async Task<Video> GetByIdIncludingDeletedAsync(int Id)
        {
            return await _context.Videos
                            .Include(v => v.Youtuber)
                            .Where(v => v.Id == Id)
                            .FirstOrDefaultAsync();
        }

        public async Task RestoreAsync(int Id)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(v => v.Id == Id);
            if (video != null && video.IsDeleted)
            {
                video.IsDeleted = false;
                video.DeletedAt = null;
                video.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task HardDeleteAsync(int Id)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(v => v.Id == Id);
            if (video != null)
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }
    }
}