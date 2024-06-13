using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Helpers;
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
                UpdateJobCommandValidator validator = new();
                ValidationResult validationResult = validator.Validate(request);

                var errors = ValidationHelper.ConvertValidationErrorsToDictionary(validationResult);
                if (!errors.IsNullOrEmpty())
                {
                    return new BaseResponse(false, "No fue posible crear la tarea", errors);
                }
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
