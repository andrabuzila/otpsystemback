namespace otpsystemback.Models
{
    public class UserToken
    {
        public string? Token { get; set; }

        public TimeSpan Validaty { get; set; }

        public string? RefreshToken { get; set; }

        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public DateTime ExpiredTime { get; set; }
    }
}
