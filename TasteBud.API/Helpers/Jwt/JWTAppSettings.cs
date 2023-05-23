namespace TasteBud.API.Helpers.Jwt
{
    // Represents settings needed to generate a JWT token 
    public class JWTAppSettings
    {
        // Property: Valid Audience
        // Represents the valid audience for the JWT token
        public string ValidAudience { get; set; }

        // Property: Valid Issuer
        // Represents the valid issuer for the JWT token
        public string ValidIssuer { get; set; }

        // Property: Secret
        // Represents the secret key used for JWT token generation and verification
        public string Secret { get; set; }
    }
}