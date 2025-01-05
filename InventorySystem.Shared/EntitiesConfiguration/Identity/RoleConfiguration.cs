using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.RoleDescription)
                .HasMaxLength(500);

            builder.HasIndex(r => r.RoleName)
                .IsUnique();
        }
    }
}
