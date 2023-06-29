using CraveIt.API.ViewModels;

namespace CraveIt.API.Services.UserService
{
    // Interface that defines the necessary implementation for user authentication operations
    public interface IUserService
    {
        // Perform user registration and automatically login user
        // Return a task that represents an asynchronous operation and resolves to a GeneralResponseViewModel
        Task<GeneralResponseViewModel> Register(RegisterViewModel registerViewModel);

        // Perform user login
        // Return a task that represents an asynchronous operation and resolves to a GeneralResponseViewModel
        Task<GeneralResponseViewModel> Login(LoginViewModel loginViewModel);
    }
}
