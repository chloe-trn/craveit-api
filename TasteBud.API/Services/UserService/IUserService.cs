using TasteBud.API.ViewModels;
using TasteBud.API.ViewModels.Login;
using TasteBud.API.ViewModels.Register;

namespace TasteBud.API.Services.UserService
{
    // Interface that defines the necessary implementation for user authentication operations
    public interface IUserService
    {
        // Definition: Performs user registration
        // Param: RegisterViewModel 
        // Return: a Task that represents an asynchronous operation and resolves to a UserResponseViewModel
        Task<GeneralResponseViewModel> Register(RegisterViewModel registerViewModel);

        // Definition: Performs user login
        // Param: LoginViewModel 
        // Return: a Task that represents an asynchronous operation and resolves to a UserResponseViewModel
        Task<GeneralResponseViewModel> Login(LoginViewModel loginViewModel);
    }
}
