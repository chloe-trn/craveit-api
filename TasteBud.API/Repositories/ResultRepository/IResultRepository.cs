using TasteBud.API.Models;

namespace TasteBud.API.Repositories.ResultRepository
{
    // Interface that defines the necessary implementation for database operations on the Result table
    public interface IResultRepository
    {
        // Retrieves a list of results based on the authenticated user
        Task<List<Result>> GetResults(string authenticatedUserId);

        // Retrieves a specific result by its ID based on the authenticated user
        Task<Result> GetResultById(int id, string authenticatedUserId);

        // Adds a new result to the Result table
        Task AddResult(Result result);

        // Deletes a result from Result table
        Task DeleteResult(Result result);
    }
}
