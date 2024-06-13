using MediatR;
using TODO_User.Application.Commons.Bases.Response;

namespace TODO_User.Application.Feature.Commands.DeleteJob
{
    public class DeleteJobCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
