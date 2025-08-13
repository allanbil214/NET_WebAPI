using YoutubeAPI.Mappings;
using YoutubeAPI.Mapping;

namespace YoutubeAPI.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(VideoMappingProfile), typeof(YoutuberMappingProfile));
            return services;
        }
    }
}