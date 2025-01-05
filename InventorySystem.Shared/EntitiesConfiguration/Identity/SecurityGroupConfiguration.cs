using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class SecurityGroupConfiguration : IEntityTypeConfiguration<SecurityGroup>
    {
        public void Configure(EntityTypeBuilder<SecurityGroup> builder)
        {
            builder.ToTable("SecurityGroups", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(sg => sg.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(sg => sg.Description)
                .HasMaxLength(500);
        }
    }
}
