using Microsoft.EntityFrameworkCore;
using TasteBud.API.Data;
using TasteBud.API.Models;

namespace TasteBud.API.Repositories.QuizRepository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly TasteBudApiDbContext _context;

        public QuizRepository(TasteBudApiDbContext context)
        {
            _context = context;
        }

        // Retrieves a list of quizzes based on the authenticated user
        public async Task<List<Quiz>> GetQuizzes(string authenticatedUserId)
        {
            return await _context.Quiz
                .Include(u => u.Results)
                .Where(q => q.UserId == authenticatedUserId)
                .ToListAsync();
        }

        // Retrieves a specific quiz by its ID based on the authenticated user
        public async Task<Quiz> GetQuizById(int id, string authenticatedUserId)
        {
            return await _context.Quiz
                .FirstOrDefaultAsync(q => q.Id == id && q.UserId == authenticatedUserId);
        }

        // Adds a new quiz to the Quiz table
        public async Task AddQuiz(Quiz quiz)
        {
            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();
        }
    }
}
