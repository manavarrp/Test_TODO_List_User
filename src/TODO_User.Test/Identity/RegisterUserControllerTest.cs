using AutoFixture;
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
    public class RegisterUserControllerTest
    {
        private Mock<IAccountApplication> _accountApplicationMock;
        private Fixture _fixture;
        private AccountController _controller;

        public RegisterUserControllerTest()
        {
            _accountApplicationMock = new Mock<IAccountApplication>();
            _fixture = new Fixture();
            _controller = new AccountController(_accountApplicationMock.Object);
        }

        [TestMethod]
        public async Task Create_User_ReturningOk()
        {
            // Arrange
            var user = _fixture.Create<CreateUserRequestDTO>();
            var expectedResponse = new BaseResponse(true, "Cuenta Creada");

            _accountApplicationMock.Setup(r => r.CreateUserAsync(It.IsAny<CreateUserRequestDTO>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(user) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as BaseResponse;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Flag);
            Assert.AreEqual("Cuenta Creada", response.Message);
        }
        [TestMethod]
        public async Task Create_User_ReturningError()
        {
            // Arrange
            var user = _fixture.Create<CreateUserRequestDTO>();
            var expectedResponse = new BaseResponse(false, "No fue posible crear al usuario", new Dictionary<string, string> { { "Error", "Detalles del error" } });

            _accountApplicationMock.Setup(r => r.CreateUserAsync(It.IsAny<CreateUserRequestDTO>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(user) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode); 
            var response = result.Value as BaseResponse;
            Assert.IsNotNull(response);
            Assert.IsFalse(response.Flag);
            Assert.AreEqual("No fue posible crear al usuario", response.Message);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual("Detalles del error", response.Errors["Error"]);
        }
    }
    
}
