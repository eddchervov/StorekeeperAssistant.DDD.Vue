﻿using System;

namespace StorekeeperAssistant.UseCases.InventoryItems.Queries.GetInventoryItems
{
    public class InventoryItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
