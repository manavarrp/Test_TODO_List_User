using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using TODO_User.Application.Interface;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Infrastructure.Persistence.Repository
{
    public class JobRepository : CommonRepository<Job>, IJobApplication
    {
       private readonly IdentityContext _identityContext;

        public JobRepository(IdentityContext dbContext) : base(dbContext) 
        {
            _identityContext = dbContext;
        }

        public async Task<IEnumerable<Job>> GetJobByEmail( string userEmail)
        {
            var jobList = await _identityContext.Jobs
                .Where(j => j.CreatedBy == userEmail)
                .ToListAsync();
            return jobList;
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            return await _identityContext.Jobs.FindAsync(id);
        }
    }
}
