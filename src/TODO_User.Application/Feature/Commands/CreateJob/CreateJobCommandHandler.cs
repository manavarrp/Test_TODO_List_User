using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Helpers;
using TODO_User.Application.Interface;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Application.Feature.Commands.CreateJob
{
    /// <summary>
    /// Manejador de comandos para crear una nueva tarea (Job).
    /// </summary>
    internal class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, BaseResponse>
    {
        private readonly IJobApplication _jobApplication;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateJobCommandHandler(IJobApplication jobApplication, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _jobApplication = jobApplication;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validar la solicitud utilizando FluentValidation
                CreateJobCommandValidator validator = new();
                ValidationResult validationResult = validator.Validate(request);

                var errors = ValidationHelper.ConvertValidationErrorsToDictionary(validationResult);

                if (!errors.IsNullOrEmpty())
                {
                    return new BaseResponse(false, "No fue posible crear la tarea", errors);
                }


                // Obtener el email del usuario desde el token JWT
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst("Email")?.Value;

                var job = _mapper.Map<Job>(request);
                job.CreatedBy = userEmail!;
                job.CreatedAt = DateTime.Now;
                job.State = 0;
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
