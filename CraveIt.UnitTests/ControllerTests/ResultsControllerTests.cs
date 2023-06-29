using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoFixture;
using Moq;
using CraveIt.API.Controllers;
using CraveIt.API.Models;
using CraveIt.API.Repositories.ResultRepository;

namespace CraveIt.UnitTests.ControllerTests
{
    public class ResultsControllerTests
    {
        private ResultsController _resultsController;
        private Mock<IResultRepository> _mockResultRepository;
        private Fixture _fixture;

        public ResultsControllerTests()
        {
            _fixture = new Fixture();
            _mockResultRepository = new Mock<IResultRepository>();
        }

        [Fact]
        public async Task GetResult_Returns_Results_When_Available()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockResultsList = _fixture.Create<List<Result>>();

            _mockResultRepository.Setup(repo => repo.GetResults(mockAuthenticatedUserId))
                .ReturnsAsync(mockResultsList);

            _resultsController = new ResultsController(_mockResultRepository.Object);
            _resultsController.ControllerContext = new ControllerContext
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
            var result = await _resultsController.GetResult();
            var okResult = result.Result as OkObjectResult;
            var results = okResult.Value as List<Result>;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<Result>>(results);
            Assert.Equal(mockResultsList, results);
        }

        [Fact]
        public async Task GetQuiz_Returns_NotFound_When_NoQuizzesAvailable()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";

            _mockResultRepository.Setup(repo => repo.GetResults(mockAuthenticatedUserId))
                .ReturnsAsync((List<Result>)null);

            _resultsController = new ResultsController(_mockResultRepository.Object);
            _resultsController.ControllerContext = new ControllerContext
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
            var result = await _resultsController.GetResult();
            var notFoundResult = result.Result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("No results available", notFoundResult.Value);
        }

        [Fact]
        public async Task GetResultById_Returns_Result_When_Available()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockResult = _fixture.Create<Result>();
            var mockResultId = 1;

            _mockResultRepository.Setup(repo => repo.GetResultById(mockResultId, mockAuthenticatedUserId))
                .ReturnsAsync(mockResult);

            _resultsController = new ResultsController(_mockResultRepository.Object);
            _resultsController.ControllerContext = new ControllerContext
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
            var result = await _resultsController.GetResult(mockResultId);
            var objectResult = result.Result as ObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(mockResult, objectResult.Value);
        }

        [Fact]
        public async Task GetResultById_Returns_NotFound_When_Result_Not_Available()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockResult = _fixture.Create<Result>();
            var mockResultId = 1;

            _mockResultRepository.Setup(repo => repo.GetResultById(mockResultId, mockAuthenticatedUserId))
                .ReturnsAsync((Result)null);

            _resultsController = new ResultsController(_mockResultRepository.Object);
            _resultsController.ControllerContext = new ControllerContext
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
            var result = await _resultsController.GetResult(mockResultId);
            var notFoundResult = result.Result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("This result isn't available", notFoundResult.Value);
        }

        [Fact]
        public async Task Delete_Returns_NoContent_When_Result_Deleted_Successfully()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockResult = _fixture.Create<Result>();
            var mockResultId = 1;

            _mockResultRepository.Setup(repo => repo.GetResultById(mockResultId, mockAuthenticatedUserId))
                .ReturnsAsync(mockResult);

            _resultsController = new ResultsController(_mockResultRepository.Object);
            _resultsController.ControllerContext = new ControllerContext
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
            var result = await _resultsController.Delete(mockResultId);
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task Delete_Returns_NotFound_When_Result_Not_Available()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockResultId = 1;

            _mockResultRepository.Setup(repo => repo.GetResultById(mockResultId, mockAuthenticatedUserId))
                .ReturnsAsync((Result)null);

            _resultsController = new ResultsController(_mockResultRepository.Object);
            _resultsController.ControllerContext = new ControllerContext
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
            var result = await _resultsController.Delete(mockResultId);
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Delete_Returns_InternalServerError_When_Failed_To_Delete_Result()
        {
            // Arrange
            var mockAuthenticatedUserId = "user1";
            var mockResult = _fixture.Create<Result>();
            var mockResultId = 1;
            var mockErrorMessage = "Failed to delete result";

            _mockResultRepository.Setup(repo => repo.GetResultById(mockResultId, mockAuthenticatedUserId))
                .ReturnsAsync(mockResult);
            _mockResultRepository.Setup(repo => repo.DeleteResult(mockResult))
                .ThrowsAsync(new Exception(mockErrorMessage));

            _resultsController = new ResultsController(_mockResultRepository.Object);
            _resultsController.ControllerContext = new ControllerContext
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
            var result = await _resultsController.Delete(mockResultId);
            var statusCodeResult = result as ObjectResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal(mockErrorMessage, statusCodeResult.Value);
        }

    }
}
