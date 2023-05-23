using System.ComponentModel.DataAnnotations;

namespace TasteBud.API.ViewModels.Register
{
    // Represents the information needed for a user registration
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}