using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class UserFeaturePermissionConfiguration : IEntityTypeConfiguration<UserFeaturePermission>
    {
        public void Configure(EntityTypeBuilder<UserFeaturePermission> builder)
        {
            builder.ToTable("UserFeaturePermissions", "Configurations");

            builder.ConfigureMinEntity();

            builder.HasKey(ufp => new { ufp.UserId, ufp.FeaturePermissionId });

            builder.Property(ufp => ufp.UserId)
                .IsRequired();

            builder.Property(ufp => ufp.FeaturePermissionId)
                .IsRequired();

            builder.HasOne(ufp => ufp.User)
                .WithMany(u => u.UserFeaturePermissions)
                .HasForeignKey(ufp => ufp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ufp => ufp.FeaturePermission)
                .WithMany(fp => fp.UserFeaturePermissions)
                .HasForeignKey(ufp => ufp.FeaturePermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
