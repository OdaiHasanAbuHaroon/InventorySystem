using InventorySystem.Shared.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class SeriLogEventConfiguration : IEntityTypeConfiguration<SeriLogEvent>
    {
        public void Configure(EntityTypeBuilder<SeriLogEvent> builder)
        {
            // Specify the table name and schema
            builder.ToTable("SeriLogEvents", "dbo");

            // Configure the base entity properties (Id, CreatedDate, etc.)
            builder.ConfigureBaseEntity();

            // Configure each property for SeriLogEvent
            builder.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(2000); // Adjust size as needed

            builder.Property(e => e.MessageTemplate)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(e => e.Level)
                .IsRequired()
                .HasMaxLength(100);

            // For 'Exception', 'LogEvent', and 'Properties', we assume they can be large.
            // Use NVARCHAR(MAX) if you need to store potentially large text.
            builder.Property(e => e.Exception)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.LogEvent)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Properties)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            // Store the timestamp with the appropriate precision for your needs
            builder.Property(e => e.TimeStamp)
                .IsRequired();
        }
    }
}
