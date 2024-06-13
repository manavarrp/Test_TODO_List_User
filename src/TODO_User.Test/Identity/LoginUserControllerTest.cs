using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TODO_User.Api.Controllers;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Dto.Identity;
using TODO_User.Application.Interface.Identity;

namespace TODO_User.Test.Identity
{
    [TestClass]
    public class LoginUserControllerTest
    {
        private Mock<IAccountApplication> _accountApplicationMock;
        private AccountController _controller;

        [TestInitialize]
        public void Setup()
        {
            _accountApplicationMock = new Mock<IAccountApplication>();
            _controller = new AccountController(_accountApplicationMock.Object);
        }

      
        [TestMethod]
        public async Task Login_ValidUser_ReturnsToken()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "test@example.com",
                Password = "password"
            };

            var expectedToken = "fake-token";
            var expectedResponse = new LoginResponse(true, expectedToken, "Inicio de Sesion");

            _accountApplicationMock.Setup(r => r.LoginAccount(It.IsAny<LoginDTO>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginDTO) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as LoginResponse;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Flag);
            Assert.AreEqual(expectedToken, response.Token);
            Assert.AreEqual("Inicio de Sesion", response.Message);
        }


        [TestMethod]
        public async Task Login_UserNotFound_ReturnsError()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "nonexistent@example.com",
                Password = "password"
            };

            var expectedResponse = new LoginResponse(false, null, "Usuario no encontrado");

            _accountApplicationMock.Setup(r => r.LoginAccount(It.IsAny<LoginDTO>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginDTO) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as LoginResponse;
            Assert.IsNotNull(response);
            Assert.IsFalse(response.Flag);
            Assert.AreEqual("Usuario no encontrado", response.Message);
        }
        [TestMethod]
        public async Task Login_InvalidPassword_ReturnsError()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "test@example.com",
                Password = "wrongpassword"
            };

            var expectedResponse = new LoginResponse(false, null, "Usuario y/o contraseña invalidos");

            _accountApplicationMock.Setup(r => r.LoginAccount(It.IsAny<LoginDTO>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(loginDTO) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode); 
            var response = result.Value as LoginResponse;
            Assert.IsNotNull(response);
            Assert.IsFalse(response.Flag);
            Assert.AreEqual("Usuario y/o contraseña invalidos", response.Message);
        }



    }
}
