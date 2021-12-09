using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.MovingAggregate;
using StorekeeperAssistant.Domain.WarehouseAggregate;

namespace StorekeeperAssistant.DataAccess.EntityConfigurations
{
    public class MovingEntityTypeConfiguration : IEntityTypeConfiguration<Moving>
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

            builder.Property<WarehouseId>("ArrivalWarehouseId")
                .HasConversion(
                i => i.Value,
                i => new WarehouseId(i)
                );

            builder.Property<WarehouseId>("DepartureWarehouseId")
                .HasConversion(
                i => i.Value,
                i => new WarehouseId(i)
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
}
