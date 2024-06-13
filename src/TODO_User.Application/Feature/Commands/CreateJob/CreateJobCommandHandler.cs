using AutoMapper;
using MediatR;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Interface;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Application.Feature.Commands.CreateJob
{
    internal class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, BaseResponse>
    {
        private readonly IJobApplication _jobApplication;
        private readonly IMapper _mapper;

        public CreateJobCommandHandler(IJobApplication jobApplication, IMapper mapper)
        {
            _jobApplication = jobApplication;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var job = _mapper.Map<Job>(request);
                job.CreatedAt = DateTime.Now;
                await _jobApplication.AddAsync(job);
                return new BaseResponse(true, "Tarea creada");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, $"Error al crear la tarea: {ex.Message}");
            }
        }
    }
}
