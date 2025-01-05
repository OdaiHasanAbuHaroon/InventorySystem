using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Code)
                   .IsRequired()
                   .HasMaxLength(3);

            builder.Property(c => c.Symbol)
                   .IsRequired()
                   .HasMaxLength(10);
        }
    }
}
