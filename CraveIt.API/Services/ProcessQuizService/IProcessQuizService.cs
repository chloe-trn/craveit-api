using CraveIt.API.Models;

namespace CraveIt.API.Services.ProcessQuizService
{
    // Interface that defines the necessary implementation for processing a user quiz
    public interface IProcessQuizService
    {
        // Process a user quiz and retrieve a quiz result
        Task<object> ProcessQuiz(Quiz quiz, string authenticatedUserId);
    }
}
