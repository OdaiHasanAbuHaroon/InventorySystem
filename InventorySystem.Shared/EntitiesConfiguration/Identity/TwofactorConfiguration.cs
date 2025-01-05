using InventorySystem.Shared.Entities.Configuration;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class TwofactorConfiguration : IEntityTypeConfiguration<Twofactor>
    {
        public void Configure(EntityTypeBuilder<Twofactor> builder)
        {
            builder.ToTable("Twofactors", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(tf => tf.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(tf => tf.ExpirationDate)
                .IsRequired()
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("DATEADD(MINUTE, 5, GETUTCDATE())");

            builder.Property(tf => tf.IsUsed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(tf => tf.IsSent)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(tf => tf.Stamp)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(tf => tf.UserId)
                .IsRequired();

            builder.Property(tf => tf.RequestType).HasDefaultValue(1)
                .IsRequired();

            builder.HasOne(tf => tf.User)
                .WithMany(u => u.Twofactors)
                .HasForeignKey(tf => tf.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
