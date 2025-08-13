using YoutubeAPI.Models.Entities;

namespace YoutubeAPI.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}