using MediatR;
using TODO_User.Application.Commons.Bases.Response;

namespace TODO_User.Application.Feature.Commands.UpdateJob
{
    public class UpdateJobCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
        public int State { get; set; }
    }
}
