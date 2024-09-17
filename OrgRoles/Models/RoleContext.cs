using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrgRoles.Configuration;
using OrgRoles.Interceptors;

namespace OrgRoles.Models
{
    public class RoleContext:DbContext
    {

        public RoleContext(DbContextOptions<RoleContext> options) : base(options)
        {
            //this.EnsureSeeData();
        }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     //       base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder
       .AddInterceptors(
       new EFSaveChangesInterceptor()
         ); // Loggin interceptor

        
    }

}
