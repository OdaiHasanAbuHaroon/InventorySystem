using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class UserModuleConfiguration : IEntityTypeConfiguration<UserModule>
    {
        public void Configure(EntityTypeBuilder<UserModule> builder)
        {
            builder.ToTable("UserModules", "Configurations");

            builder.ConfigureMinEntity();

            builder.HasKey(um => new { um.UserId, um.ModuleId });

            builder.Property(um => um.UserId)
                .IsRequired();

            builder.Property(um => um.ModuleId)
                .IsRequired();

            builder.HasOne(um => um.User)
                .WithMany(u => u.UserModules)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(um => um.Module)
                .WithMany(m => m.UserModules)
                .HasForeignKey(um => um.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
