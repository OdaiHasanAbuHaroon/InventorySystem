using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="Supplier"/> entity's properties,
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        /// <summary>
        /// Configures the <see cref="Supplier"/> entity, including its table name, schema,
        /// property constraints, and relationships.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="Supplier"/> entity.</param>
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            // Table name and schema
            builder.ToTable("Suppliers", "Application");

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

            // Configure the foreign key relationship for Country.
            // A Supplier is associated with exactly one Country 
            // (with many Suppliers potentially referencing the same Country).
            builder.HasOne(s => s.Country)
                .WithMany()
                .HasForeignKey(s => s.CountryId);
        }
    }
}
