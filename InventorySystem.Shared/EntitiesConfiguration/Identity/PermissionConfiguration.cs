using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
