using MediatR;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Interface;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Application.Feature.Commands.DeleteJob
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, BaseResponse>
    {
        private readonly IJobApplication _jobApplication;
        public DeleteJobCommandHandler(IJobApplication jobApplication )
        {
            _jobApplication = jobApplication;
          
        }

        public async Task<BaseResponse> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        { 
            try
            {
                await _jobApplication.DeleteAsync(new Job() { Id = request.Id });
                return new BaseResponse(true, "Tarea eliminada");
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, $"Error al eliminar la tarea: {ex.Message}");
            }

        }
    }
}
