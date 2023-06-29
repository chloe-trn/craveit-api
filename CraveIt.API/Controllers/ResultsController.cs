using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CraveIt.API.Models;
using CraveIt.API.Repositories.ResultRepository;

namespace CraveIt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;

        // Constructor of UserController
        // Param: instance of IUserService
        public ResultsController(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        // GET: api/results
        // Endpoint to retrieve all saved results for a logged in user 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResult()
        {
            // Retrieve the authenticated user's Id from claims
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Call ResultRepository to get all results for a user
            var resultsList = await _resultRepository.GetResults(authenticatedUserId);

            // Check if no results are found
            if (resultsList == null || resultsList.Count == 0)
            {
                return NotFound("No results available");
            }

            // Return the results in an Ok response
            return Ok(resultsList);
        }

        // GET api/results/{id}
        // Endpoint to retrieve a saved result by Id for a logged in user 
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            // Retrieve the authenticated user's Id from claims
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Call ResultRepository to get result by Id
            var result = await _resultRepository.GetResultById(id, authenticatedUserId);

            // Check if result not found
            if (result == null)
            {
                return NotFound("This result isn't available");
            }

            // Return the result in an Ok response
            return Ok(result);
        }

        // DELETE api/results/{id}
        // Endpoint to delete a saved result by Id for a logged in user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Retrieve the authenticated user's Id from claims
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Call ResultRepository to get result by Id
            var result = await _resultRepository.GetResultById(id, authenticatedUserId);

            // Check if result not found
            if (result == null)
            {
                return NotFound();
            }
            
            try
            {
                // Call ResultRepository to delete the result
                await _resultRepository.DeleteResult(result);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/results
        // Endpoint to add a saved result by Id for a logged in user
        [HttpPost]
        public async Task<ActionResult<Result>> AddResult(Result result)
        {
            // Retrieve the authenticated user's Id from claims
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Set who the result belongs to by assigning the authenticated user's Id
            // to the result's UserId property
            result.UserId = authenticatedUserId;

            try
            {
                // Call ResultRepository to add the result
                await _resultRepository.AddResult(result);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
