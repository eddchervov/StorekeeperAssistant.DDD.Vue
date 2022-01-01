using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.Domain.MovingAggregate.MovingDetails
{
    public class MovingDetailCount : ValueObject
    {
        public int Value { get; }

        private MovingDetailCount()
        {

        }

        public MovingDetailCount(int count)
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
