using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.Description)
                .HasMaxLength(500);

            builder.Property(f => f.ModuleId)
                .IsRequired();

            builder.HasIndex(f => f.Name)
                .IsUnique();

            builder.HasOne(f => f.Module)
                .WithMany(m => m.Features)
                .HasForeignKey(f => f.ModuleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
