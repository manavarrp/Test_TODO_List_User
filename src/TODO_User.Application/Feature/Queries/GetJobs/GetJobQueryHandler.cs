using AutoMapper;
using MediatR;
using TODO_User.Application.Interface;

namespace TODO_User.Application.Feature.Queries.GetJobs
{
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
            var jobs = await _jobApplication.GetJobs();
            return _mapper.Map<List<GetJobsDto>>(jobs);

        }
    }
}
