using FluentValidation;
using YoutubeAPI.Models.DTOs;

namespace YoutubeAPI.Validators
{
    public class VideoUpdateDTOValidator : AbstractValidator<VideoUpdateDTO>
    {
        public VideoUpdateDTOValidator()
        {
            RuleFor(x => x.Title)
                .Length(1, 200).WithMessage("Title must be between 1 and 200 characters.")
                .When(x => x.Title != null);

            RuleFor(x => x.Description)
                .MaximumLength(5000).WithMessage("Description cannot exceed 5000 characters.")
                .When(x => x.Description != null);

            RuleFor(x => x.Url)
                .Must(BeAValidUrl).WithMessage("Please provide a valid URL.")
                .Must(BeAYouTubeUrl).WithMessage("URL must be a valid YouTube URL.")
                .When(x => x.Url != null);

            RuleFor(x => x.ViewCount)
                .GreaterThanOrEqualTo(0).WithMessage("View count cannot be negative.")
                .When(x => x.ViewCount.HasValue);

            RuleFor(x => x.LikeCount)
                .GreaterThanOrEqualTo(0).WithMessage("Like count cannot be negative.")
                .When(x => x.LikeCount.HasValue);

            RuleFor(x => x.DislikeCount)
                .GreaterThanOrEqualTo(0).WithMessage("Dislike count cannot be negative.")
                .When(x => x.DislikeCount.HasValue);

            RuleFor(x => x.YoutuberID)
                .GreaterThan(0).WithMessage("Valid Youtuber ID is required.")
                .When(x => x.YoutuberID.HasValue);
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? result) 
                && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        private bool BeAYouTubeUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            
            var uri = new Uri(url);
            return uri.Host.Contains("youtube.com") || uri.Host.Contains("youtu.be");
        }
    }
}