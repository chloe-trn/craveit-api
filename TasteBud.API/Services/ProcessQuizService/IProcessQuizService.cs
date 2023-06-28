using TasteBud.API.Models;
using TasteBud.API.ViewModels.YelpViewModels;

namespace TasteBud.API.Services.ProcessQuizService
{
    // Interface that defines the necessary implementation for processing a user quiz
    public interface IProcessQuizService
    {
        // Processes a user quiz and retrieves a quiz result
        Task<object> ProcessQuiz(Quiz quiz, string authenticatedUserId);
    }
}
