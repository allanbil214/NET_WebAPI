namespace YoutubeAPI.Models.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public UserResponseDTO User { get; set; }
    }
}