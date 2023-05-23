namespace TasteBud.API.ViewModels.Login
{
    // Represents the response to a user login request
    public class LoginResponseViewModel
    {
        // A Jwt token provided for a user's session
        public string Token { get; set; }

        // The expiration date of the Jwt token
        public DateTime Expiration { get; set; }

        // A message in the body of the response
        public string Message { get; set; } 
    }
}