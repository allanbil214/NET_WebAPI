namespace YoutubeAPI.Models.DTOs
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}