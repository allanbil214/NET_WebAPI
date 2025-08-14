namespace YoutubeAPI.Models.Entities
{
    public class Youtuber
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string ChannelName { get; set; } 
        public string Email { get; set; } 
        public int Subscriber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}