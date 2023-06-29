using CraveIt.API.Models;
using CraveIt.API.Repositories.QuizRepository;
using CraveIt.API.Repositories.ResultRepository;
using CraveIt.API.Services.RandomizerService;
using CraveIt.API.Services.YelpService;
using CraveIt.API.ViewModels;

namespace CraveIt.API.Services.ProcessQuizService
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

        // Process a user quiz and retrieve a quiz result
        public async Task<object> ProcessQuiz(Quiz quiz, string authenticatedUserId)
        {
            // Add quiz to the database
            _quizRepository.AddQuiz(quiz);

            // Call the GetYelpData method to retrieve the Yelp API response
            YelpResponseViewModel yelpResponse = await _yelpService.GetYelpData(quiz);

            if (!yelpResponse.Businesses.Any()) // Check if no businesses are present in the list
            {
                throw new Exception("No businesses found with this criteria");
            }

            // Pass the Yelp API response to the randomizer service
            Business result = _randomizerService.GetRandomListItem(yelpResponse.Businesses);

            // Create the result object
            var resultObject = new
            {
                QuizId = quiz.Id,
                Business = result
            };

            return resultObject;
        }
    }
}
