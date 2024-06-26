﻿using AutoMapper;
using MediatR;
using TODO_User.Application.Interface;

namespace TODO_User.Application.Feature.Queries.GetJobs
{
    /// <summary>
    /// Manejador de consultas para obtener las tareas (jobs) de un usuario por su correo electrónico.
    /// </summary>
    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, List<GetJobsDto>>
    {
        private readonly IJobApplication _jobApplication;
        private readonly IMapper _mapper;

        public GetJobQueryHandler(IJobApplication jobApplication, IMapper mapper)
        {
            _jobApplication = jobApplication;
            _mapper = mapper;
        }

        public async Task<List<GetJobsDto>> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobApplication.GetJobByEmail(request.UserEmail);
            return _mapper.Map<List<GetJobsDto>>(jobs);

        }
    }
}
