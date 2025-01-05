using InventorySystem.Shared.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class AttachmentBackupConfiguration : IEntityTypeConfiguration<AttachmentBackup>
    {
        public void Configure(EntityTypeBuilder<AttachmentBackup> builder)
        {
            builder.ToTable("AttachmentBackups", "Configurations");

            builder.HasKey(ab => ab.Id);
            builder.Property(ab => ab.Id).ValueGeneratedOnAdd();

            builder.Property(ab => ab.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(ab => ab.Extention)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(ab => ab.Base64Content)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(ab => ab.AttachmentId)
                   .IsRequired();

            builder.HasOne(ab => ab.Attachment)
                   .WithMany(ia => ia.AttachmentBackups)
                   .HasForeignKey(ab => ab.AttachmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
