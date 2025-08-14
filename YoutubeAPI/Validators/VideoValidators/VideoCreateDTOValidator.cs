using FluentValidation;
using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Validators
{
    public class VideoCreateDTOValidator : AbstractValidator<VideoCreateDTO>
    {
        public VideoCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 200).WithMessage("Title must be between 1 and 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(5000).WithMessage("Description cannot exceed 5000 characters.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL is required.")
                .Must(BeAValidUrl).WithMessage("Please provide a valid URL.")
                .Must(BeAYouTubeUrl).WithMessage("URL must be a valid YouTube URL.");

            RuleFor(x => x.ViewCount)
                .GreaterThanOrEqualTo(0).WithMessage("View count cannot be negative.");

            RuleFor(x => x.LikeCount)
                .GreaterThanOrEqualTo(0).WithMessage("Like count cannot be negative.");

            RuleFor(x => x.DislikeCount)
                .GreaterThanOrEqualTo(0).WithMessage("Dislike count cannot be negative.");

            RuleFor(x => x.YoutuberID)
                .GreaterThan(0).WithMessage("Valid Youtuber ID is required.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? result) 
                   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        private bool BeAYouTubeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                return false;

            return uri.Host.Contains("youtube.com") || uri.Host.Contains("youtu.be");
        }
    }
}