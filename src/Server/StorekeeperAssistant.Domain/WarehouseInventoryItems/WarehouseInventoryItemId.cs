﻿using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.WarehouseInventoryItems;

public sealed class WarehouseInventoryItemId : EntityId
{
    public WarehouseInventoryItemId(Guid value) : base(value)
    {
    }
}
