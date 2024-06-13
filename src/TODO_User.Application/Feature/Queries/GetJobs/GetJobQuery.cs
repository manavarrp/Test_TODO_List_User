using MediatR;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Application.Feature.Queries.GetJobs
{
    public class GetJobQuery : IRequest<List<GetJobsDto>>
    {
    }
}
