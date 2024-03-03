using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.Warehouses;

#nullable disable

namespace StorekeeperAssistant.DataAccess.EntityConfigurations;

sealed class WarehouseEntityTypeConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            i => i.Value,
            i => new WarehouseId(i)
            );

        builder.OwnsOne(x => x.Name, d =>
        {
            d.Property(b => b.Value).HasColumnName("Name");
        });

        builder.Property(b => b.IsDeleted).HasColumnName("IsDeleted");
    }
}
