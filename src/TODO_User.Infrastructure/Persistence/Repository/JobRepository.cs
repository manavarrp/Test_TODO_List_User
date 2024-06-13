using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using TODO_User.Application.Interface;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Infrastructure.Persistence.Repository
{
    /// <summary>
    /// Repositorio para la gestión de entidades Job utilizando Entity Framework Core.
    /// </summary>
    public class JobRepository : CommonRepository<Job>, IJobApplication
    {
       private readonly IdentityContext _identityContext;

        public JobRepository(IdentityContext dbContext) : base(dbContext) 
        {
            _identityContext = dbContext;
        }

        /// <summary>
        /// Método para obtener una lista de trabajos por correo electrónico del creador.
        /// </summary>
        public async Task<IEnumerable<Job>> GetJobByEmail( string userEmail)
        {
            var jobList = await _identityContext.Jobs
                .Where(j => j.CreatedBy == userEmail)
                .ToListAsync();
            return jobList;
        }

        /// <summary>
        /// Método para obtener un trabajo por su identificador único.
        /// </summary>
        public async Task<Job> GetByIdAsync(int id)
        {
            return await _identityContext.Jobs.FindAsync(id);
        }
    }
}
