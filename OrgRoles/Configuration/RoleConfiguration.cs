using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgRoles.Models;

namespace OrgRoles.Configuration
{
    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasOne(r => r.Parent)
                 .WithMany()
                 .HasForeignKey(r => r.ParentId)
                 .IsRequired(false)
                 .HasConstraintName("FK_ROLE_PARENT");
            builder.Property(r => r.Name).IsRequired(true)
                .HasMaxLength(20);
            builder.Property(r=>r.Description).IsRequired(true)
                .HasMaxLength(150);

        }
    }
}
