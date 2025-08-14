using FluentValidation;
using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Validators
{
    public class YoutuberUpdateDTOValidator : AbstractValidator<YoutuberUpdateDTO>
    {
        public YoutuberUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.")
                .Matches(@"^[a-zA-Z\s\-'\.]+$").WithMessage("Name can only contain letters, spaces, hyphens, apostrophes, and periods.")
                .When(x => x.Name != null);

            RuleFor(x => x.ChannelName)
                .Length(3, 50).WithMessage("Channel name must be between 3 and 50 characters.")
                .Matches(@"^[a-zA-Z0-9\s\-_\.]+$").WithMessage("Channel name can only contain letters, numbers, spaces, hyphens, underscores, and periods.")
                .When(x => x.ChannelName != null);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Please provide a valid email address.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.")
                .When(x => x.Email != null);

            RuleFor(x => x.Subscriber)
                .GreaterThanOrEqualTo(0).WithMessage("Subscriber count cannot be negative.")
                .LessThanOrEqualTo(1000000000).WithMessage("Subscriber count seems unrealistic. Please verify.")
                .When(x => x.Subscriber.HasValue);
        }
    }
}