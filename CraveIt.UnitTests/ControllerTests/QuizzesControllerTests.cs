using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AutoFixture;
using Moq;
using CraveIt.API.Controllers;
using CraveIt.API.Models;
using CraveIt.API.Services.ProcessQuizService;
using CraveIt.API.Repositories.QuizRepository;
using CraveIt.API.ViewModels;

namespace CraveIt.UnitTests.ControllerTests
{
    public class QuizzesControllerTests
    {
        private QuizzesController _quizzesController;
        private Mock<IQuizRepository> _mockQuizRepository;
        private Mock<IProcessQuizService> _mockProcessQuizService;
        private Fixture _fixture;

        public QuizzesControllerTests()
        {
            _fixture = new Fixture();
            _mockQuizRepository = new Mock<IQuizRepository>();
            _mockProcessQuizService = new Mock<IProcessQuizService>();
        }

        [Fact]
        public async Task GetQuiz_Returns_Quizzes_When_Available()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockQuizzesList = _fixture.Create<List<Quiz>>();

            _mockQuizRepository.Setup(repo => repo.GetQuizzes(mockAuthenticatedUserId))
                .ReturnsAsync(mockQuizzesList);

            _quizzesController = new QuizzesController(_mockQuizRepository.Object, _mockProcessQuizService.Object);
            _quizzesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, mockAuthenticatedUserId)
                    }))
                }
            };

            // Act
            var result = await _quizzesController.GetQuiz();
            var okResult = result.Result as OkObjectResult;
            var quizzes = okResult.Value as List<Quiz>;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<Quiz>>(quizzes);
            Assert.Equal(mockQuizzesList, quizzes);
        }

        [Fact]
        public async Task GetQuiz_Returns_NotFound_When_NoQuizzesAvailable()
        {
            // Arrange
            string mockAuthenticatedUserId = "user1";

            _mockQuizRepository.Setup(repo => repo.GetQuizzes(mockAuthenticatedUserId))
                .ReturnsAsync((List<Quiz>)null);

            _quizzesController = new QuizzesController(_mockQuizRepository.Object, _mockProcessQuizService.Object);
            _quizzesController.ControllerContext = new ControllerContext();
            _quizzesController.ControllerContext.HttpContext = new DefaultHttpContext();
            _quizzesController.ControllerContext.HttpContext.User = new ClaimsPrincipal(
                new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, mockAuthenticatedUserId)
                }));

            // Act
            var result = await _quizzesController.GetQuiz();
            var notFoundResult = result.Result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("No quizzes available", notFoundResult.Value);
        }

        [Fact]
        public async Task GetQuizById_Returns_Quiz_When_Available()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockQuiz = _fixture.Create<Quiz>();
            var mockQuizId = 1; 
            
            _mockQuizRepository.Setup(repo => repo.GetQuizById(mockQuizId, mockAuthenticatedUserId))
                .ReturnsAsync(mockQuiz);

            _quizzesController = new QuizzesController(_mockQuizRepository.Object, _mockProcessQuizService.Object);
            _quizzesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, mockAuthenticatedUserId)
                    }))
                }
            };

            // Act
            var result = await _quizzesController.GetQuiz(mockQuizId);
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(mockQuiz, objectResult.Value);
        }

        [Fact]
        public async Task GetQuizById_Returns_NotFound_When_Quiz_Not_Available()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockQuiz = _fixture.Create<Quiz>();
            var mockQuizId = 1;

            _mockQuizRepository.Setup(repo => repo.GetQuizById(mockQuizId, mockAuthenticatedUserId))
                .ReturnsAsync((Quiz)null);

            _quizzesController = new QuizzesController(_mockQuizRepository.Object, _mockProcessQuizService.Object);
            _quizzesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, mockAuthenticatedUserId)
                    }))
                }
            };

            // Act
            var result = await _quizzesController.GetQuiz(mockQuizId);
            var notFoundResult = result.Result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("This quiz isn't available", notFoundResult.Value);
        }

        [Fact]
        public async Task PostQuiz_Returns_Ok_With_QuizResult_When_Quiz_Processed_Successfully()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockQuiz = _fixture.Create<Quiz>();
            var mockQuizResult = _fixture.Create<Business>();

            _mockProcessQuizService.Setup(service => service.ProcessQuiz(mockQuiz, mockAuthenticatedUserId))
                .ReturnsAsync(mockQuizResult);

            _quizzesController = new QuizzesController(_mockQuizRepository.Object, _mockProcessQuizService.Object);
            _quizzesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, mockAuthenticatedUserId)
                    }))
                }
            };

            // Act
            var result = await _quizzesController.PostQuiz(mockQuiz);
            var okResult = result.Result as OkObjectResult;
            var quizResponse = okResult.Value as Business;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<Business>(quizResponse);
            Assert.Equal(mockQuizResult, quizResponse);
        }

        [Fact]
        public async Task PostQuiz_Returns_InternalServerError_When_Quiz_Processing_Fails()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockQuiz = _fixture.Create<Quiz>();
            var mockErrorMessage = "Failed to retrieve Yelp data and retrieve a result";

            _mockProcessQuizService.Setup(service => service.ProcessQuiz(mockQuiz, mockAuthenticatedUserId))
                .ThrowsAsync(new Exception(mockErrorMessage));

            _quizzesController = new QuizzesController(_mockQuizRepository.Object, _mockProcessQuizService.Object);
            _quizzesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, mockAuthenticatedUserId)
                    }))
                }
            };

            // Act
            var result = await _quizzesController.PostQuiz(mockQuiz);
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal(mockErrorMessage, objectResult.Value);
        }
    }
}
