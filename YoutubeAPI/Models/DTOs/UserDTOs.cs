namespace YoutubeAPI.Models.DTOs
{
    public class UserRegisterDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Roles { get; set; } = string.Empty;
    }

    public class UserLoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public UserResponseDTO User { get; set; }
    }
}