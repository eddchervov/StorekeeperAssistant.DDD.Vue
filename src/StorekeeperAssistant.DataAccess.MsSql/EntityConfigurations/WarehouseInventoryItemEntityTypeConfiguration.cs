﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StorekeeperAssistant.Domain.InventoryItemAggregate;
using StorekeeperAssistant.Domain.MovingAggregate;
using StorekeeperAssistant.Domain.WarehouseAggregate;
using StorekeeperAssistant.Domain.WarehouseInventoryItemAggregate;

namespace StorekeeperAssistant.DataAccess.EntityConfigurations
{
    class WarehouseInventoryItemEntityTypeConfiguration : IEntityTypeConfiguration<WarehouseInventoryItem>
    {
        public void Configure(EntityTypeBuilder<WarehouseInventoryItem> builder)
        {
            builder.ToTable("WarehouseInventoryItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                i => i.Value,
                i => new WarehouseInventoryItemId(i)
                );

            builder.Property(b => b.Count).HasColumnName("Count");

            builder.Property(b => b.Date).HasColumnName("Date");

            builder.Property<InventoryItemId>("InventoryItemId")
                .HasConversion(
                i => i.Value,
                i => new InventoryItemId(i)
                );

            builder.Property<MovingId>("MovingId")
                .HasConversion(
                i => i.Value,
                i => new MovingId(i)
                );

            builder.Property<WarehouseId>("WarehouseId")
                .HasConversion(
                i => i.Value,
                i => new WarehouseId(i)
                );
        }
    }
}