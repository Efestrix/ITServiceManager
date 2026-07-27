namespace ITServiceManager.API.Dtos.Authentication
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
