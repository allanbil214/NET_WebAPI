namespace YoutubeAPI.Models.DTOs
{
    public class VideoReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public int YoutuberID { get; set; }
        public YoutuberReadDTO Youtuber { get; set; }
    }

    public class VideoCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;

        public int YoutuberID { get; set; }
    }

    public class VideoUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public int? ViewCount { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }

        public int? YoutuberID { get; set; }
    }

    public class VideoDeleteDTO
    {
        public bool IsDeleted { get; set; }

    }

    public class VideoAdminReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public DateTime PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int YoutuberID { get; set; }

        public YoutuberAdminReadDTO Youtuber { get; set; }
    }
}