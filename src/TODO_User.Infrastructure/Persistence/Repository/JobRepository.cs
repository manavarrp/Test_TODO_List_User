using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var jobList = await _identityContext.Jobs.ToListAsync();
            return jobList;
        }
    }
}
