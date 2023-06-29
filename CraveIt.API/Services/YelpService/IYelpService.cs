using CraveIt.API.Models;
using CraveIt.API.ViewModels;

namespace CraveIt.API.Services.YelpService
{
    // Interface that defines the necessary implementation for external Yelp connection
    public interface IYelpService
    {
        // Perform Yelp API call
        Task<YelpResponseViewModel> GetYelpData(Quiz quiz);
    }
}
