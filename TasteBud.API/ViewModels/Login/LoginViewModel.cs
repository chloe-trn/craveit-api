using System.ComponentModel.DataAnnotations;

namespace TasteBud.API.ViewModels.Login
{
    // Represents the information needed for a user login
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}