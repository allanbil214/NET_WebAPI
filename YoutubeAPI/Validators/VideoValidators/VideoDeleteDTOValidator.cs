using FluentValidation;
using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Validators
{
    public class VideoDeleteDTOValidator : AbstractValidator<VideoDeleteDTO>
    {
        public VideoDeleteDTOValidator()
        {
            RuleFor(x => x.IsDeleted)
                .NotNull().WithMessage("IsDeleted flag is required.");
        }
    }
}