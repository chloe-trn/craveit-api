using Microsoft.EntityFrameworkCore;
using CraveIt.API.Data;
using CraveIt.API.Models;

namespace CraveIt.API.Repositories.QuizRepository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly CraveItApiDbContext _context;

        public QuizRepository(CraveItApiDbContext context)
        {
            _context = context;
        }

        // Retrieve a list of quizzes based on the authenticated user
        public async Task<List<Quiz>> GetQuizzes(string authenticatedUserId)
        {
            return await _context.Quiz
                .Include(u => u.Results)
                .Where(q => q.UserId == authenticatedUserId)
                .ToListAsync();
        }

        // Retrieve a specific quiz by its ID based on the authenticated user
        public async Task<Quiz> GetQuizById(int id, string authenticatedUserId)
        {
            return await _context.Quiz
                .FirstOrDefaultAsync(q => q.Id == id && q.UserId == authenticatedUserId);
        }

        // Add a new quiz to the Quiz table
        public async Task AddQuiz(Quiz quiz)
        {
            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();
        }
    }
}
