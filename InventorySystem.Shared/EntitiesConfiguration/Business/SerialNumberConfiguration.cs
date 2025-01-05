using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="SerialNumber"/> entity's properties,
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class SerialNumberConfiguration : IEntityTypeConfiguration<SerialNumber>
    {
        /// <summary>
        /// Configures the <see cref="SerialNumber"/> entity, including its table name, schema,
        /// property constraints, and relationships.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="SerialNumber"/> entity.</param>
        public void Configure(EntityTypeBuilder<SerialNumber> builder)
        {
            // Table name and schema
            builder.ToTable("SerialNumbers", "Application");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            // Configure 'Serial' property
            builder.Property(sn => sn.Serial)
                .IsRequired()
                .HasMaxLength(200);

            // Create a unique index on 'Serial'
            builder.HasIndex(sn => sn.Serial)
                .IsUnique();

            /// <summary>
            /// Defines the relationship between a SerialNumber and an Item.
            /// A SerialNumber must be linked to exactly one Item.
            /// Uses cascading deletes when the linked Item is removed.
            /// </summary>
            builder.HasOne(sn => sn.Item)
                .WithMany()
                .HasForeignKey(sn => sn.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
