using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TODO_User.Application.Feature.Commands.CreateJob;
using TODO_User.Application.Feature.Commands.DeleteJob;
using TODO_User.Application.Feature.Commands.UpdateJob;
using TODO_User.Application.Feature.Queries.GetJobs;

namespace TODO_User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var orders = await _mediator.Send(new GetJobQuery());
            return Ok(orders);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrateJob([FromBody]  CreateJobCommand command)
        {

            var response = await _mediator.Send(command);
            return Ok(response);
           
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateJob([FromBody] UpdateJobCommand command)
        {

            var response = await _mediator.Send(command);
            return Ok(response);

        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var response = await _mediator.Send(new DeleteJobCommand { Id = id });
            return Ok(response);
        }

    }
}
