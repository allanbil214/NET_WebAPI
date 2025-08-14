namespace YoutubeAPI.Models.DTOs
{
    public class VideoCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public int YoutuberID { get; set; }
    }
}