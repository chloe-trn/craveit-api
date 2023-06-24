using TasteBud.API.Models;
using TasteBud.API.ViewModels.YelpViewModels;

namespace TasteBud.API.Services.YelpService
{
    // Interface that defines the necessary implementation for external Yelp connection
    public interface IYelpService
    {
        // Performs Yelp API call 
        // TODO: Pass in a QuizResponseViewModel, use that information in the call
        Task<YelpResponseViewModel> GetYelpData(Quiz quiz);
    }
}
