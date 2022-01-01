using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.Domain.MovingAggregate.WarehouseInventoryItems
{
    public class WarehouseInventoryItemCount : ValueObject
    {
        public int Value { get; }

        private WarehouseInventoryItemCount()
        {

        }

        public WarehouseInventoryItemCount(int count)
        {
            if (count < 1)
                throw new ArgumentException("MovingDetail can`t be less then 1!", nameof(count));

            Value = count;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
