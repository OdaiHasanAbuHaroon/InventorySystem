using InventorySystem.Shared.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration
{
    public static class EntityConfigurationExtensions
    {
        public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CreatedDate)
                   .IsRequired()
                   .HasColumnType("datetime2(7)")
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.ModifiedDate)
                   .HasColumnType("datetime2(7)");

            builder.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(e => e.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(e => e.Source).HasMaxLength(200).IsRequired(false);
        }

        public static void ConfigureIBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedNever();

            builder.Property(e => e.CreatedDate)
                   .IsRequired()
                   .HasColumnType("datetime2(7)")
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.ModifiedDate)
                   .HasColumnType("datetime2(7)");

            builder.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(e => e.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(e => e.Source).HasMaxLength(200).IsRequired(false);
        }

        public static void ConfigureMinEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : MinEntity
        {
            // BaseEntity properties
            builder.Property(e => e.CreatedDate)
                   .IsRequired()
                   .HasColumnType("datetime2(7)")
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(e => e.Source).HasMaxLength(200).IsRequired(false);
        }
    }
}
