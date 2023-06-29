using Microsoft.EntityFrameworkCore;
using CraveIt.API.Data;
using CraveIt.API.Models;

namespace CraveIt.API.Repositories.ResultRepository
{
    public class ResultRepository : IResultRepository
    {
        private readonly CraveItApiDbContext _context;

        public ResultRepository(CraveItApiDbContext context)
        {
            _context = context;
        }

        // Retrieve a list of results based on the authenticated user
        public async Task<List<Result>> GetResults(string authenticatedUserId)
        {
            var groupedResults = await _context.Result
               .Where(r => r.UserId == authenticatedUserId)
               .GroupBy(r => r.RestaurantName)
               .ToListAsync();

            var sortedResults = groupedResults
                .SelectMany(g => g.OrderByDescending(r => r.Date))
                .OrderBy(r => r.Date)  
                .ToList();

            return sortedResults;
        }

        // Retrieve a specific result by its ID based on the authenticated user
        public async Task<Result> GetResultById(int id, string authenticatedUserId)
        {
            return await _context.Result
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == authenticatedUserId);
        }

        // Add a new result to the Result table
        public async Task AddResult(Result result)
        {
            _context.Result.Add(result);
            await _context.SaveChangesAsync();
        }

        // Delete a result from Result table
        public async Task DeleteResult(Result result)
        {
            _context.Result.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
