using TasteBud.API.Models;
using TasteBud.API.Repositories.QuizRepository;
using TasteBud.API.Repositories.ResultRepository;
using TasteBud.API.Services.RandomizerService;
using TasteBud.API.Services.YelpService;
using TasteBud.API.ViewModels.YelpViewModels;

namespace TasteBud.API.Services.ProcessQuizService
{
    public class ProcessQuizService : IProcessQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IYelpService _yelpService;
        private readonly IRandomizerService _randomizerService;

        public ProcessQuizService(IQuizRepository quizRepository,
            IResultRepository resultRepository,
            IYelpService yelpService,
            IRandomizerService randomizerService)
        {
            _quizRepository = quizRepository;
            _resultRepository = resultRepository;
            _yelpService = yelpService;
            _randomizerService = randomizerService;
        }

        public async Task<Business> ProcessQuiz(Quiz quiz, string authenticatedUserId)
        {
            // Add quiz to the database
            _quizRepository.AddQuiz(quiz);

            // Call the GetYelpData method to retrieve the Yelp API response
            YelpResponseViewModel yelpResponse = await _yelpService.GetYelpData(); // TODO: pass quiz parameters

            if (!yelpResponse.Businesses.Any()) // No businesses are present in the list
            {
                throw new Exception("No businesses found.");
            }

            // Pass the Yelp API response to the randomizer service
            Business result = _randomizerService.GetRandomListItem(yelpResponse.Businesses);

            // Create a new Result object and assign values to its properties
            Result resultEntry = new Result
            {
                Date = DateTime.Now,
                RestaurantName = result.Name,
                Location = $"{result.Location.Address1} {result.Location.City} {result.Location.State}, {result.Location.ZipCode}",
                PriceRange = result.Price,
                QuizId = quiz.Id,
                UserId = authenticatedUserId
            };

            // Add quiz result to the database
            _resultRepository.AddResult(resultEntry);

            return result;
        }
    }
}
