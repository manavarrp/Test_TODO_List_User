using AutoMapper;
using MediatR;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Interface;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Application.Feature.Commands.UpdateJob
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, BaseResponse>
    {
        private readonly IJobApplication _jobApplication;
        private readonly IMapper _mapper;

        public UpdateJobCommandHandler(IJobApplication jobApplication, IMapper mapper)
        {
            _jobApplication = jobApplication;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            

            try
            {
                var job = _mapper.Map<Job>(request);
                job.LastUpdated = DateTime.Now;
                await _jobApplication.UpdateAsync(job);
                return new BaseResponse(true, "Tarea Actualizada");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, $"Error al Actualizar la tarea: {ex.Message}");
            }
        }
    }
}
