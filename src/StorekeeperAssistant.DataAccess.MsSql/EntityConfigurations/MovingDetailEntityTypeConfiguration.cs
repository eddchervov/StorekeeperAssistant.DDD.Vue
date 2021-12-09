using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.MovingAggregate;

namespace StorekeeperAssistant.DataAccess.EntityConfigurations
{
    class MovingDetailEntityTypeConfiguration : IEntityTypeConfiguration<MovingDetail>
    {
        public void Configure(EntityTypeBuilder<MovingDetail> builder)
        {
            builder.ToTable("MovingDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                i => i.Value,
                i => new MovingDetailId(i)
                );

            builder.OwnsOne(p => p.Count, p =>
            {
                p.Property(pv => pv.Count).HasColumnName("Count");
            })
            .Navigation(x => x.Count).IsRequired();

            builder.Property<InventoryItemId>("InventoryItemId")
                .HasConversion(
                i => i.Value,
                i => new InventoryItemId(i)
                );

            builder.Property<MovingId>("MovingId")
                .HasConversion(
                i => i.Value,
                i => new MovingId(i)
                )
                .IsRequired();
        }
    }
}
