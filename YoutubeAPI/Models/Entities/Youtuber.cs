namespace YoutubeAPI.Models.Entities
{
    public class Youtuber
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ChannelName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Subscriber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}