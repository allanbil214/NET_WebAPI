using FluentValidation;
using YoutubeAPI.Models.DTOs;
using YoutubeAPI.Validators;

namespace YoutubeAPI.Extensions
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            // Register all validators
            services.AddScoped<IValidator<VideoCreateDTO>, VideoCreateDTOValidator>();
            services.AddScoped<IValidator<VideoUpdateDTO>, VideoUpdateDTOValidator>();
            services.AddScoped<IValidator<VideoDeleteDTO>, VideoDeleteDTOValidator>();

            services.AddScoped<IValidator<YoutuberCreateDTO>, YoutuberCreateDTOValidator>();
            services.AddScoped<IValidator<YoutuberUpdateDTO>, YoutuberUpdateDTOValidator>();
            services.AddScoped<IValidator<YoutuberDeleteDTO>, YoutuberDeleteDTOValidator>();

            return services;
        }
    }
}