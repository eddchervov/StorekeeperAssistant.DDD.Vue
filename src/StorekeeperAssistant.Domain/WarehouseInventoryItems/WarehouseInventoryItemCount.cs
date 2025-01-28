using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.Domain.WarehouseInventoryItems;

public sealed class WarehouseInventoryItemCount : ValueObject
{
    public int Value { get; }

    private WarehouseInventoryItemCount()
    {

    }

    public WarehouseInventoryItemCount(int count)
    {
        if (count < 0)
            throw new ArgumentException("Count can`t be less then 0!", nameof(count));

        Value = count;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
