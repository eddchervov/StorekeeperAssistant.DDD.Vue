using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.InventoryItemAggregate;

namespace StorekeeperAssistant.DataAccess.EntityConfigurations
{
    public class InventoryItemEntityTypeConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.ToTable("InventoryItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                i => i.Value,
                i => new InventoryItemId(i)
                );

            builder.OwnsOne(x => x.Name, d =>
            {
                d.Property(b => b.Value).HasColumnName("Name");
            });

            builder.Property(b => b.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}