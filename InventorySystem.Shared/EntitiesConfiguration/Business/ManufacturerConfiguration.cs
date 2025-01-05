using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="Manufacturer"/> entity's properties,
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        /// <summary>
        /// Configures the <see cref="Manufacturer"/> entity, including its table name, schema,
        /// property constraints, and relationships.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="Manufacturer"/> entity.</param>
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            // Table name and schema
            builder.ToTable("Manufacturers", "Application");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            // Configure Name
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Optionally add a unique index on the Name property
            builder.HasIndex(s => s.Name)
                .IsUnique();

            // Configure ContactEmail
            builder.Property(s => s.ContactEmail)
                .HasMaxLength(200);

            // Configure ContactName
            builder.Property(s => s.ContactName)
                .HasMaxLength(200);

            // Configure ContactNumber
            builder.Property(s => s.ContactNumber)
                .HasMaxLength(50);

            // Configure Address
            builder.Property(s => s.Address)
                .HasMaxLength(500);

            // Configure Description
            builder.Property(s => s.Description)
                .HasMaxLength(1000);

            /// <summary>
            /// Defines the relationship between a Manufacturer and a Country.
            /// A Manufacturer is associated with exactly one Country 
            /// (with many Manufacturers potentially referencing the same Country).
            /// </summary>
            builder.HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId);
        }
    }
}
