namespace TechnestHackhaton.Application.DTOs
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expration { get; set; }
        public string RefreshToken { get; set; }
    }
}
