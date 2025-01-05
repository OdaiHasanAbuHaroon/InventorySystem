using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="Category"/> entity's properties, 
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Configures the <see cref="Category"/> entity, including table name/schema,
        /// property constraints, and indexes.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="Category"/> entity.</param>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table name and schema
            builder.ToTable("Categories", "Application");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            /// <summary>
            /// The 'Name' property of the category (required, max length 200).
            /// </summary>
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            /// <summary>
            /// The 'Description' property of the category (optional, max length 500).
            /// </summary>
            builder.Property(c => c.Description)
                .HasMaxLength(500);

            /// <summary>
            /// Creates a unique index on the 'Name' property 
            /// to ensure no two categories share the same name.
            /// </summary>
            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
