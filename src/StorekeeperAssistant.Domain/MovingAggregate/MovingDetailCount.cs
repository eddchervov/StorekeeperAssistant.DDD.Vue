using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace StorekeeperAssistant.Domain.MovingAggregate
{
    public class MovingDetailCount : ValueObject
    {
        public int Count { get; }

        private MovingDetailCount()
        {

        }

        public MovingDetailCount(int count)
        {
            if (count < 0)
                throw new ArgumentException("MovingDetail can`t be less then 0!", nameof(count));

            Count = count;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Count;
        }
    }
}
