using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TODO_User.Api.Controllers;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Feature.Commands.CreateJob;
using TODO_User.Application.Feature.Commands.DeleteJob;
using TODO_User.Application.Feature.Commands.UpdateJob;
using TODO_User.Application.Feature.Queries.GetJobs;
using TODO_User.Application.Interface.Identity;

namespace TODO_User.Tests
{

    [TestClass]
    public class JobApplicationTests
    {
        private Mock<IMediator> _mediatorMock;
        private Fixture _fixture;
        private JobController _controller;

        public JobApplicationTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _fixture = new Fixture();
            _controller = new JobController(_mediatorMock.Object);
        }
        [TestMethod]
        public async Task GetAllJobs_ReturnsJobsForAuthenticatedUser()
        {
            // Arrange
            var userEmail = "test@example.com"; // Usuario autenticado
            var expectedJobs = new List<GetJobsDto>
            {
                new GetJobsDto { Id = 1, Name = "Job 1" },
                new GetJobsDto { Id = 2, Name = "Job 2" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetJobQuery>(), default)).ReturnsAsync(expectedJobs);

            // Act
            var result = await _controller.GetAllJobs(userEmail) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var jobs = result.Value as List<GetJobsDto>;
            Assert.IsNotNull(jobs);
            Assert.AreEqual(expectedJobs.Count, jobs.Count);
        }

        [TestMethod]
        public async Task CreateJob_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = _fixture.Create<CreateJobCommand>(); 
            var expectedResponse = new BaseResponse(true, "Tarea creada");

            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CrateJob(command) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as BaseResponse;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Flag);
            Assert.AreEqual("Tarea creada", response.Message);
        }
        [TestMethod]
        public async Task UpdateJob_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new UpdateJobCommand { Id = 1,  State = 1 };
            var expectedResponse = new BaseResponse(true, "Tarea actualizada");

            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.UpdateJob(command) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as BaseResponse;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Flag);
            Assert.AreEqual("Tarea actualizada", response.Message);
        }
        [TestMethod]
        public async Task DeleteJob_ValidId_ReturnsOk()
        {
            // Arrange
            int jobIdToDelete = 1;
            var expectedResponse = new BaseResponse(true, "Tarea eliminada");

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteJobCommand>(), default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.DeleteJob(jobIdToDelete) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var response = result.Value as BaseResponse;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Flag);
            Assert.AreEqual("Tarea eliminada", response.Message);
        }
    }
}


