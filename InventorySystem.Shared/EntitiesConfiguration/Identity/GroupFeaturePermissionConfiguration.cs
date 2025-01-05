using InventorySystem.Shared.Entities.Configuration.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class GroupFeaturePermissionConfiguration : IEntityTypeConfiguration<GroupFeaturePermission>
    {
        public void Configure(EntityTypeBuilder<GroupFeaturePermission> builder)
        {
            builder.ToTable("GroupFeaturePermissions", "Configurations");

            builder.Property(gfp => gfp.SecurityGroupId)
                .IsRequired();

            builder.Property(gfp => gfp.FeaturePermissionId)
                .IsRequired();

            builder.HasKey(gfp => new { gfp.FeaturePermissionId, gfp.SecurityGroupId });

            builder.HasOne(gfp => gfp.FeaturePermission)
                .WithMany(fp => fp.GroupFeaturePermissions)
                .HasForeignKey(gfp => gfp.FeaturePermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gfp => gfp.SecurityGroup)
                .WithMany(sg => sg.GroupFeaturePermissions)
                .HasForeignKey(gfp => gfp.SecurityGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
