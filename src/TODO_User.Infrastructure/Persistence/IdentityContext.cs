using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TODO_User.Domain.Entities.Identity;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Infrastructure.Persistence
{
    public  class IdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options) { }

        public DbSet<Job> Jobs { get; set; }

       
    }
}
