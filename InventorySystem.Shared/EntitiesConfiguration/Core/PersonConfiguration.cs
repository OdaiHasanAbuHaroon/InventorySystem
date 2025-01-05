using InventorySystem.Shared.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Shared.EntitiesConfiguration.Core
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons", "Configurations");

            builder.ConfigureBaseEntity();

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.HasIndex(e => e.Name).IsUnique();

            builder.Property(e => e.Reference)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.HasIndex(e => e.Reference).IsUnique();

            builder.Property(e => e.Type)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Cost)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.UserId)
                   .IsRequired();

            // Define relationship for User
            builder.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
