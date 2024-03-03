using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.Movings;
using StorekeeperAssistant.Domain.Warehouses;

#nullable disable

namespace StorekeeperAssistant.DataAccess.EntityConfigurations;

sealed class MovingEntityTypeConfiguration : IEntityTypeConfiguration<Moving>
{
    public void Configure(EntityTypeBuilder<Moving> builder)
    {
        builder.ToTable("Movings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            i => i.Value,
            i => new MovingId(i)
            );

        builder.Property<ArrivalWarehouseId>("ArrivalWarehouseId")
            .HasConversion(
            i => i.Value,
            i => new ArrivalWarehouseId(i)
            );

        builder.Property<DepartureWarehouseId>("DepartureWarehouseId")
            .HasConversion(
            i => i.Value,
            i => new DepartureWarehouseId(i)
            );

        builder.Property(b => b.TransferDate).HasColumnName("TransferDate");

        builder.Property(b => b.IsDeleted).HasColumnName("IsDeleted");

        builder.HasMany(x => x.MovingDetails)
           .WithOne()
           .HasForeignKey("MovingId")
           .OnDelete(DeleteBehavior.Cascade);

        var navigation = builder.Metadata.FindNavigation(nameof(Moving.MovingDetails));

        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
