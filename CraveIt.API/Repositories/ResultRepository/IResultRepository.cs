using CraveIt.API.Models;

namespace CraveIt.API.Repositories.ResultRepository
{
    // Interface that defines the necessary implementation for database operations on the Result table
    public interface IResultRepository
    {
        // Retrieve a list of results based on the authenticated user
        Task<List<Result>> GetResults(string authenticatedUserId);

        // Retrieve a specific result by its ID based on the authenticated user
        Task<Result> GetResultById(int id, string authenticatedUserId);

        // Add a new result to the Result table
        Task AddResult(Result result);

        // Delete a result from Result table
        Task DeleteResult(Result result);
    }
}
