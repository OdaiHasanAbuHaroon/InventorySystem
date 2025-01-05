using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages", "Configurations");

            builder.ConfigureIBaseEntity();

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.NativeName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.Code)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(l => l.NameAr)
                   .HasMaxLength(100);
        }
    }
}
