using InventorySystem.Shared.Entities.Configuration.Identity;
using InventorySystem.Shared.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.MiddleName)
                .HasMaxLength(500);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.TwoFactorEnabled)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.AccessFaildCount)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(u => u.LookoutEnabled)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.Lookout)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.EmailConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.MobileNumberConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.SmsEnabled)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(u => u.LookoutEnd)
                .HasColumnType("datetime2(7)");

            builder.Property(u => u.LastLoginDate)
                .HasColumnType("datetime2(7)");

            builder.Property(u => u.Configuration)
                .HasMaxLength(500);

            builder.Property(u => u.Signature)
                .HasMaxLength(500);

            builder.Property(u => u.LastPasswordUpdate)
                .HasColumnType("datetime2(7)");

            builder.Property(u => u.ImageId);
        }
    }
}
