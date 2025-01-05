using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="Brand"/> entity's properties,
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        /// <summary>
        /// Configures the <see cref="Brand"/> entity, including its table name, schema,
        /// property constraints, and relationships.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="Brand"/> entity.</param>
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            // Table name and schema
            builder.ToTable("Brands", "Application");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            /// <summary>
            /// The 'Name' property for the brand (required, max length 200).
            /// </summary>
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(200);

            /// <summary>
            /// The 'Description' property for the brand (optional, max length 500).
            /// </summary>
            builder.Property(c => c.Description)
                .HasMaxLength(500);

            /// <summary>
            /// Defines the relationship between a Brand and a Manufacturer.
            /// Each Brand must have exactly one Manufacturer.
            /// Uses cascade deletion when the linked Manufacturer is removed.
            /// </summary>
            builder.HasOne(b => b.Manufacturer)
                .WithMany()
                .HasForeignKey(b => b.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
