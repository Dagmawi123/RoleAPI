using Microsoft.EntityFrameworkCore;
using OrgRoles.Configuration;

namespace OrgRoles.Models
{
    public class RoleContext:DbContext
    {
        public RoleContext(DbContextOptions<RoleContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     //       base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleContext).Assembly);
        }

    }

}
