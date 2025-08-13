using Microsoft.EntityFrameworkCore;
using YoutubeAPI.Repositories.Interfaces;
using YoutubeAPI.Data;
using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Repositories.Implementations
{
    public class YoutuberRepository : IYoutuberRepository
    {
        private readonly AppDbContext _context;
        public YoutuberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Youtuber>> GetAllAsync()
        {
            var result = await _context.Youtubers
                            .Include(y => y.Videos)
                            .Where(y => y.IsDeleted == false)
                            .ToListAsync();
            return result;
        }

        public async Task<Youtuber> GetByIdAsync(int Id)
        {
            var result = await _context.Youtubers
                            .Include(y => y.Videos)
                            .Where(y => y.Id == Id)
                            .Where(y => y.IsDeleted == false)
                            .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Youtuber> AddAsync(Youtuber Youtuber)
        {
            Youtuber.CreatedAt = DateTime.UtcNow;
            _context.Youtubers.Add(Youtuber);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(Youtuber.Id);
        }

        public async Task DeleteAsync(int Id)
        {
            var category = await _context.Youtubers.FirstOrDefaultAsync(y => y.Id == Id);
            if (category != null)
            {
                category.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

        }

        public async Task EditAsync(Youtuber Youtuber)
        {
            Youtuber.UpdatedAt = DateTime.UtcNow;
            _context.Entry(Youtuber).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> YoutuberExistsAsync(int Id)
        {
            var result = await _context.Youtubers.AnyAsync(y => y.Id == Id);
            return result;
        }

        public async Task<List<Youtuber>> GetAllIncludingDeletedAsync()
        {
            return await _context.Youtubers
                            .Include(y => y.Videos)
                            .ToListAsync();
        }

        public async Task<Youtuber> GetByIdIncludingDeletedAsync(int Id)
        {
            return await _context.Youtubers
                            .Include(y => y.Videos)
                            .Where(y => y.Id == Id)
                            .FirstOrDefaultAsync();
        }

        public async Task RestoreAsync(int Id)
        {
            var youtuber = await _context.Youtubers.FirstOrDefaultAsync(y => y.Id == Id);
            if (youtuber != null && youtuber.IsDeleted)
            {
                youtuber.IsDeleted = false;
                youtuber.DeletedAt = null;
                youtuber.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task HardDeleteAsync(int Id)
        {
            var youtuber = await _context.Youtubers.FirstOrDefaultAsync(y => y.Id == Id);
            if (youtuber != null)
            {
                _context.Youtubers.Remove(youtuber);
                await _context.SaveChangesAsync();
            }
        }
    }
}