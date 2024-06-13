using EF.Core.Repository.Interface.Repository;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Application.Interface
{
    public interface IJobApplication : ICommonRepository<Job>
    {
        Task<IEnumerable<Job>> GetJobByEmail(string userEmail);
        Task<Job> GetByIdAsync(int id);
    }
}
