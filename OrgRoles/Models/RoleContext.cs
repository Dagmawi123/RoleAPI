using Microsoft.EntityFrameworkCore;

namespace OrgRoles.Models
{
    public class RoleContext:DbContext
    {
        public RoleContext(DbContextOptions<RoleContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Role>()
        //        .HasOne(r => r.Parent)
        //        .WithMany()
        //        .HasForeignKey(r => r.Parent)??
        //        .OnDelete(DeleteBehavior.Cascade);
        //}

    }

}
