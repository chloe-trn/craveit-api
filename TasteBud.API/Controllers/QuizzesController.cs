using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteBud.API.Models;
using TasteBud.API.Repositories.QuizRepository;
using TasteBud.API.Services.ProcessQuizService;
using TasteBud.API.ViewModels.YelpViewModels;

namespace TasteBud.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IProcessQuizService _processQuizService;

        // Constructor of QuizzesController
        // Param: instance of IQuizRepository, instace of IProcessQuizService 
        public QuizzesController(IQuizRepository quizRepository, IProcessQuizService processQuizService)
        {
            _quizRepository = quizRepository;
            _processQuizService = processQuizService;
        }

        // GET: api/quizzes
        // Endpoint to retrieve user quizzes for a logged in user 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuiz()
        {
            // Retrieve the authenticated user's ID from the claims
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Call QuizRepository to get all quizzes 
            var quizzesList = await _quizRepository.GetQuizzes(authenticatedUserId);

            // Check if no quizzes are found
            if (quizzesList == null || quizzesList.Count == 0)
            {
                return NotFound("No quizzes available");
            }

            // Return list of quizzes in an Ok response
            return Ok(quizzesList);
        }

        // GET: api/quizzes/{id}
        // Endpoint to retrieve a user quiz by Id for a logged in user 
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            // Retrieve the authenticated user's ID from the claims
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Call QuizRepository to get quiz by Id
            var quiz = await _quizRepository.GetQuizById(id, authenticatedUserId);

            // Check if quiz is not found
            if (quiz == null)
            {
                return NotFound("This quiz isn't available");
            }

            // Return quiz in an Ok response
            return Ok(quiz);
        }

        // POST: api/quizzes
        // Endpoint for a user's quiz submission
        [HttpPost]
        public async Task<ActionResult<YelpResponseViewModel>> PostQuiz(Quiz quiz)
        {
            // Retrieve the authenticated user's ID from the claims
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Set who took the quiz by assigning the authenticated user's ID
            // to the quiz's UserId property
            quiz.UserId = authenticatedUserId;
            
            try
            {
                // Call the ProcessQuizService to get a quiz result (a random business) 
                Business quizResult = await _processQuizService.ProcessQuiz(quiz, authenticatedUserId);

                // Return the result in an Ok response
                return Ok(quizResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}