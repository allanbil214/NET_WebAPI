using YoutubeAPI.Services.Interfaces;
using YoutubeAPI.Services.Implementations;

namespace YoutubeAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IYoutuberService, YoutuberService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}