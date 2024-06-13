using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TODO_User.Application.Commons.Bases.Response;
using TODO_User.Application.Helpers;
using TODO_User.Application.Interface;
using TODO_User.Domain.Entities.Users;
using System.Security.Claims;

namespace TODO_User.Application.Feature.Commands.UpdateJob
{
    /// <summary>
    /// Manejador de comandos para atulizar  el estado de una tarea (Job).
    /// </summary>
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, BaseResponse>
    {
        private readonly IJobApplication _jobApplication;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateJobCommandHandler(IJobApplication jobApplication, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _jobApplication = jobApplication;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validar el comando
                UpdateJobCommandValidator validator = new();
                ValidationResult validationResult = validator.Validate(request);

                var errors = ValidationHelper.ConvertValidationErrorsToDictionary(validationResult);
                if (!errors.IsNullOrEmpty())
                {
                    return new BaseResponse(false, "No fue posible actualizar la tarea", errors);
                }

                // Obtener el email del usuario autenticado
                var userEmail = _httpContextAccessor.HttpContext.User.FindFirst("Email")?.Value;
                if (string.IsNullOrEmpty(userEmail))
                {
                    return new BaseResponse(false, "No se pudo obtener el correo electrónico del usuario.");
                }

                // Obtener la tarea desde la base de datos
                var existingJob = await _jobApplication.GetByIdAsync(request.Id);
                if (existingJob == null)
                {
                    return new BaseResponse(false, "Tarea no encontrada.");
                }

                // Verificar si el usuario autenticado es el propietario de la tarea
                if (existingJob.CreatedBy != userEmail)
                {
                    return new BaseResponse(false, "No tienes permiso para modificar esta tarea.");
                }

                // Mapear los cambios y actualizar la tarea
                var job = _mapper.Map(request, existingJob);
                job.LastUpdated = DateTime.Now;
                await _jobApplication.UpdateAsync(job);

                return new BaseResponse(true, "Tarea actualizada");
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, $"Error al actualizar la tarea: {ex.Message}");
            }
        }
    }
}
