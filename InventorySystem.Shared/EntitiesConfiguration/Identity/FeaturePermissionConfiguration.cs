using InventorySystem.Shared.Entities.Configuration.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class FeaturePermissionConfiguration : IEntityTypeConfiguration<FeaturePermission>
    {
        public void Configure(EntityTypeBuilder<FeaturePermission> builder)
        {
            builder.ToTable("FeaturePermissions", "Configurations");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(fp => fp.FeatureId)
                .IsRequired();

            builder.Property(fp => fp.PermissionId)
                .IsRequired();

            builder.HasIndex(fp => new { fp.FeatureId, fp.PermissionId })
                .IsUnique();

            builder.HasOne(fp => fp.Feature)
                .WithMany(f => f.FeaturePermissions)
                .HasForeignKey(fp => fp.FeatureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fp => fp.Permission)
                .WithMany(p => p.FeaturePermissions)
                .HasForeignKey(fp => fp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
