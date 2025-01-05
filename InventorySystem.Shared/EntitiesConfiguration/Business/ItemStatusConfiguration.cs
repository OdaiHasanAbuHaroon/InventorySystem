using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="ItemStatus"/> entity's properties, 
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class ItemStatusConfiguration : IEntityTypeConfiguration<ItemStatus>
    {
        /// <summary>
        /// Configures the <see cref="ItemStatus"/> entity, including its table name, schema,
        /// property constraints, and indexes.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="ItemStatus"/> entity.</param>
        public void Configure(EntityTypeBuilder<ItemStatus> builder)
        {
            // Table name and schema
            builder.ToTable("ItemStatuses", "Application");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            /// <summary>
            /// The 'Name' property of the item status (required, max length 200).
            /// </summary>
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            /// <summary>
            /// The 'Description' property of the item status (optional, max length 500).
            /// </summary>
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            /// <summary>
            /// Creates a unique index on the 'Name' property 
            /// to ensure no two item statuses share the same name.
            /// </summary>
            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
