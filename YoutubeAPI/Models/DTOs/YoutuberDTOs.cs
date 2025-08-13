namespace YoutubeAPI.Models.DTOs
{
    public class YoutuberReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ChannelName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Subscriber { get; set; }
    }

    public class YoutuberCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ChannelName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Subscriber { get; set; }
    }

    public class YoutuberUpdateDTO
    {
        public string? Name { get; set; }
        public string? ChannelName { get; set; }
        public string? Email { get; set; }
        public int? Subscriber { get; set; }
    }

    public class YoutuberDeleteDTO
    {
        public bool IsDeleted { get; set; }

    }

    public class YoutuberAdminReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ChannelName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Subscriber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}