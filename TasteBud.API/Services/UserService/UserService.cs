using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TasteBud.API.Helpers.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasteBud.API.ViewModels;
using TasteBud.API.ViewModels.Login;
using TasteBud.API.ViewModels.Register;

namespace TasteBud.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTAppSettings _jWTAppSettings;

        public UserService(UserManager<IdentityUser> userManager,
            IOptions<JWTAppSettings> jWTAppSettings)
        {
            _userManager = userManager;
            _jWTAppSettings = jWTAppSettings.Value;
        }

        // Definition: Performs user registration
        // Param: RegisterViewModel 
        // Return: a Task that represents an asynchronous operation and resolves to a RegisterResponseViewModel
        public async Task<GeneralResponseViewModel> Register(RegisterViewModel registerViewModel)
        {
            // Check if the user already exists
            var userExists = await _userManager.FindByNameAsync(registerViewModel.Username);
            if (userExists != null)
            {
                return new GeneralResponseViewModel { Status = "Error", Message = "User already exists!" };
            }

            // Create a new IdentityUser object with the provided user details
            IdentityUser user = new()
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Attempt to create the user using the UserManager
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            // Return a error response if the user was NOT created successfully
            if (!result.Succeeded)
            {
                return new GeneralResponseViewModel { Status = "Error", Message = "User registration failed. Please check user details and try again." };
            }

            // Return a success response if the user was created successfully
            return new GeneralResponseViewModel { Status = "Success", Message = "User created successfully!" };
        }

        // Definition: Performs user login
        // Param: LoginViewModel 
        // Return: a Task that represents an asynchronous operation and resolves to a LoginResponseViewModel
        public async Task<LoginResponseViewModel> Login(LoginViewModel loginViewModel)
        {
            // Find user by the provided username
            var user = await _userManager.FindByNameAsync(loginViewModel.Username);

            // Check if the user exists and the provided password is correct
            if (user != null && await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                // Retrieve the roles assigned to the user
                var userRoles = await _userManager.GetRolesAsync(user);

                // Create a list of claims to be included in the authentication token
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // Add the user roles as claims to the authentication token
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Generate the JWT token using the authClaims
                var token = GetToken(authClaims);

                // Return the login response containing the generated token
                // and its expiration date
                return new LoginResponseViewModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    Message = "Login successful!"
                };
            }

            // Return an empty login response if the user is not found
            // or the password is incorrect
            return new LoginResponseViewModel
            {
                Message = "Invalid username or password."
            };
        }

        // Definition: Creates a Jwt authentication token
        // Param: authClaims (a list of user role claims) 
        // Return: a Jwt authentication token
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            // Create the symmetric security key using the secret from the JWTAppSettings
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTAppSettings.Secret));

            // Create a new JWT token with specified parameters
            var token = new JwtSecurityToken(
                issuer: _jWTAppSettings.ValidIssuer, // Set the token issuer
                audience: _jWTAppSettings.ValidAudience, // Set the token audience
                expires: DateTime.Now.AddHours(3), // Set the expiration date/time of the token (3 hours from now)
                claims: authClaims, // Set the claims to be included in the token
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256) // Set the signing credentials for token verification
                );

            return token;
        }
    }
}