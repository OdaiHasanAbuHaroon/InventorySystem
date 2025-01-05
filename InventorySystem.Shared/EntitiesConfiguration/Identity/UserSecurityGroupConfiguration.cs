using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class UserSecurityGroupConfiguration : IEntityTypeConfiguration<UserSecurityGroup>
    {
        public void Configure(EntityTypeBuilder<UserSecurityGroup> builder)
        {
            builder.ToTable("UserSecurityGroups", "Configurations");

            builder.ConfigureMinEntity();

            builder.HasKey(usg => new { usg.UserId, usg.SecurityGroupId });

            builder.Property(usg => usg.UserId)
                .IsRequired();

            builder.Property(usg => usg.SecurityGroupId)
                .IsRequired();

            builder.HasOne(usg => usg.User)
                .WithMany(u => u.UserSecurityGroups)
                .HasForeignKey(usg => usg.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(usg => usg.SecurityGroup)
                .WithMany(sg => sg.UserSecurityGroups)
                .HasForeignKey(usg => usg.SecurityGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
