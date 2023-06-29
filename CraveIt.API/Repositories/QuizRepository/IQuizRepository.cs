using CraveIt.API.Models;

namespace CraveIt.API.Repositories.QuizRepository
{
    // Interface that defines the necessary implementation for database operations on the Quiz table
    public interface IQuizRepository
    {
        // Retrieve a list of quizzes based on the authenticated user
        Task<List<Quiz>> GetQuizzes(string authenticatedUserId);

        // Retrieve a specific quiz by its ID based on the authenticated user
        Task<Quiz> GetQuizById(int id, string authenticatedUserId);

        // Add a new quiz to the Quiz table
        Task AddQuiz(Quiz quiz);
    }
}
