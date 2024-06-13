using MediatR;
using TODO_User.Application.Commons.Bases.Response;

namespace TODO_User.Application.Feature.Commands.CreateJob
{
    public class CreateJobCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
    }
}
