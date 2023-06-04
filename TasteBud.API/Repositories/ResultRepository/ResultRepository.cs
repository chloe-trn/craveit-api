using Microsoft.EntityFrameworkCore;
using TasteBud.API.Data;
using TasteBud.API.Models;

namespace TasteBud.API.Repositories.ResultRepository
{
    public class ResultRepository : IResultRepository
    {
        private readonly TasteBudApiDbContext _context;

        public ResultRepository(TasteBudApiDbContext context)
        {
            _context = context;
        }

        // Retrieves a list of results based on the authenticated user
        public async Task<List<Result>> GetResults(string authenticatedUserId)
        {
            return await _context.Result
                .Where(r => r.UserId == authenticatedUserId)
                .ToListAsync();
        }

        // Retrieves a specific result by its ID based on the authenticated user
        public async Task<Result> GetResultById(int id, string authenticatedUserId)
        {
            return await _context.Result
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == authenticatedUserId);
        }

        // Adds a new result to the Result table
        public async Task AddResult(Result result)
        {
            _context.Result.Add(result);
            await _context.SaveChangesAsync();
        }

        // Deletes a result from Result table
        public async Task DeleteResult(Result result)
        {
            _context.Result.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
