using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class ThemeConfiguration : IEntityTypeConfiguration<Theme>
    {
        public void Configure(EntityTypeBuilder<Theme> builder)
        {
            builder.ToTable("Themes", "Configurations");

            builder.ConfigureIBaseEntity();

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasIndex(t => t.Name).IsUnique();

            builder.Property(t => t.Color)
                   .HasMaxLength(50);

            builder.Property(t => t.FontSize);

            builder.Property(t => t.IsDefault)
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }
}
