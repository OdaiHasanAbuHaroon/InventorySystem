using InventorySystem.Shared.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Business
{
    /// <summary>
    /// Responsible for configuring the <see cref="Item"/> entity's properties,
    /// constraints, and relationships for the Entity Framework model.
    /// </summary>
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        /// <summary>
        /// Configures the <see cref="Item"/> entity, including table name/schema,
        /// property constraints, and relationships.
        /// </summary>
        /// <param name="builder">Provides a simple API for configuring the <see cref="Item"/> entity.</param>
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            // Table name and schema
            builder.ToTable("Items", "Application");

            // Configure the common BaseEntity properties
            builder.ConfigureBaseEntity();

            /// <summary>
            /// Required field 'Name' with a maximum length of 200.
            /// </summary>
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(200);

            /// <summary>
            /// Optional field 'Description' with a maximum length of 500.
            /// </summary>
            builder.Property(i => i.Description)
                .HasMaxLength(500);

            /// <summary>
            /// Required field 'UnitOfMeasurement' with a maximum length of 50.
            /// </summary>
            builder.Property(i => i.UnitOfMeasurement)
                .IsRequired()
                .HasMaxLength(50);

            /// <summary>
            /// Defines the relationship between an Item and a Category.
            /// Each Item must have exactly one Category, 
            /// with many Items referencing the same Category.
            /// On delete cascade behavior ensures Items are removed 
            /// if the referenced Category is removed.
            /// </summary>
            builder.HasOne(i => i.Category)
                .WithMany()
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            /// <summary>
            /// Defines the relationship between an Item and a Brand.
            /// Each Item must have exactly one Brand,
            /// with many Items referencing the same Brand.
            /// On delete cascade behavior ensures Items are removed
            /// if the referenced Brand is removed.
            /// </summary>
            builder.HasOne(i => i.Brand)
                .WithMany()
                .HasForeignKey(i => i.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            /// <summary>
            /// Defines the relationship between a SerialNumber and a Location.
            /// A SerialNumber must be linked to exactly one Location.
            /// Uses cascading deletes when the linked Location is removed.
            /// </summary>
            builder.HasOne(sn => sn.Location)
                .WithMany()
                .HasForeignKey(sn => sn.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            /// <summary>
            /// Defines the relationship between an Item and an ItemStatus.
            /// Each Item must have exactly one ItemStatus,
            /// with many Items referencing the same ItemStatus.
            /// On delete cascade behavior ensures Items are removed
            /// if the referenced ItemStatus is removed.
            /// </summary>
            builder.HasOne(i => i.ItemStatus)
                .WithMany()
                .HasForeignKey(i => i.ItemStatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
