﻿using AutoFixture;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TasteBud.API.Controllers;
using TasteBud.API.Services.UserService;
using TasteBud.API.ViewModels;
using TasteBud.API.ViewModels.Login;
using TasteBud.API.ViewModels.Register;

namespace TasteBud.UnitTests.ControllerTests
{
    public class UserControllerTests
    {
        private UserController _userController;
        private Mock<IUserService> _mockUserService; 
        private Fixture _fixture;

        public UserControllerTests()
        {
            _fixture = new Fixture();
            _mockUserService = new Mock<IUserService>();
        }

        [Fact]
        public async Task Post_Register_User_Returns_OK_With_Registration_Response_Body()
        {
            // Arrange
            var registerViewModel = _fixture.Create<RegisterViewModel>();
            var generalResponseViewModel = _fixture.Create<GeneralResponseViewModel>();
            var generalResponseViewModelTask = Task.FromResult(generalResponseViewModel);

            // Set up the mock user service to return the fake response task
            _mockUserService.Setup(repo => repo.Register(It.IsAny<RegisterViewModel>()))
                .Returns(generalResponseViewModelTask);

            // Create the controller with the mock user service
            _userController = new UserController(_mockUserService.Object);

            // Act
            var result = await _userController.Register(registerViewModel);
            var obj = result as ObjectResult;

            // Assert
            Assert.Equal(200, obj.StatusCode);
            Assert.IsType<GeneralResponseViewModel>(obj.Value);
            Assert.Equal(generalResponseViewModel, obj.Value);
        }
        
        [Fact]
        public async Task Post_Login_User_Returns_OK_With_Login_Response_Body()
        {
            // Arrange
            var loginViewModel = _fixture.Create<LoginViewModel>();
            var loginResponseViewModel = _fixture.Create<LoginResponseViewModel>();
            var loginResponseViewModelTask = Task.FromResult(loginResponseViewModel);

            // Set up the mock user service to return the fake response task
            _mockUserService.Setup(repo => repo.Login(It.IsAny<LoginViewModel>()))
                .Returns(loginResponseViewModelTask);

            // Create the controller with the mock user service
            _userController = new UserController(_mockUserService.Object);

            // Act
            var result = await _userController.Login(loginViewModel);
            var obj = result as ObjectResult;

            // Assert
            Assert.Equal(200, obj.StatusCode);
            Assert.IsType<LoginResponseViewModel>(obj.Value);
            Assert.Equal(loginResponseViewModel, obj.Value);
        }

        [Fact]
        public async Task Post_Login_User_Returns_Unauthorized_When_Login_Fails()
        {
            // Arrange
            var loginViewModel = _fixture.Create<LoginViewModel>();
            LoginResponseViewModel loginResponseViewModel = null; // Simulate a failing login
            var loginResponseViewModelTask = Task.FromResult(loginResponseViewModel);

            // Set up the mock user service to return the fake response task
            _mockUserService.Setup(repo => repo.Login(It.IsAny<LoginViewModel>()))
                .Returns(loginResponseViewModelTask);

            // Create the controller with the mock user service
            _userController = new UserController(_mockUserService.Object);

            // Act
            var result = await _userController.Login(loginViewModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
