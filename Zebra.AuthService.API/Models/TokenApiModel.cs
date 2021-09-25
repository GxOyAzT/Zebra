namespace Zebra.AuthService.API.Models
{
    public class TokenApiModel
    {
        public TokenApiModel(string email, string token)
        {
            Email = email;
            Token = token;
        }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
