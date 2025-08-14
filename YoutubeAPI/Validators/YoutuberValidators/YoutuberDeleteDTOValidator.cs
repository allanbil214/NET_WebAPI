using FluentValidation;
using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Validators
{
    public class YoutuberDeleteDTOValidator : AbstractValidator<YoutuberDeleteDTO>
    {
        public YoutuberDeleteDTOValidator()
        {
            RuleFor(x => x.IsDeleted)
                .NotNull().WithMessage("IsDeleted flag is required.");
        }
    }
}