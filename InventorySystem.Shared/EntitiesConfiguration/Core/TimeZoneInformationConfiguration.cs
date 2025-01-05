using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class TimeZoneInformationConfiguration : IEntityTypeConfiguration<TimeZoneInformation>
    {
        public void Configure(EntityTypeBuilder<TimeZoneInformation> builder)
        {
            builder.ToTable("TimeZoneInformations", "Configurations");

            builder.ConfigureIBaseEntity();

            builder.Property(tz => tz.Value)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(tz => tz.DisplayName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(tz => tz.StandardName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(tz => tz.DaylightName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(tz => tz.BaseUtcOffset)
                   .IsRequired()
                   .HasColumnType("time");

            builder.Property(tz => tz.SupportsDaylightSavingTime)
                   .IsRequired();

            builder.Property(tz => tz.UtcOffset)
                   .IsRequired();
        }
    }
}
