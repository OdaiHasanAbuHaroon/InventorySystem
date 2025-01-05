using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="Location"/> entity's properties, 
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        /// <summary>
        /// Configures the <see cref="Location"/> entity, including its table name, schema,
        /// property constraints, and indexes.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="Location"/> entity.</param>
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            // Table name and schema
            builder.ToTable("Locations", "Application");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            /// <summary>
            /// The 'Name' property of the location (required, max length 200).
            /// </summary>
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            /// <summary>
            /// The 'Type' property of the location (required, max length 100).
            /// Represents the type/category of the location.
            /// </summary>
            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(100);

            /// <summary>
            /// The 'Address' property of the location (optional, max length 500).
            /// </summary>
            builder.Property(x => x.Address)
                .HasMaxLength(500);

            /// <summary>
            /// Creates a unique index on the 'Name' property 
            /// to ensure no two locations share the same name.
            /// </summary>
            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
