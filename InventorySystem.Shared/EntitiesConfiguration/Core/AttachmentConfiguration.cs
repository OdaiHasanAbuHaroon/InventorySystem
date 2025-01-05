using InventorySystem.Shared.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            // Table name and schema
            builder.ToTable("Attachments", "Configurations");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            // Properties specific to Attachment
            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(a => a.Extention)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(a => a.Path)
                   .IsRequired()
                   .HasMaxLength(256);
        }
    }
}