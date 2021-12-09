using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.MovingAggregate
{
    public class MovingId : EntityId
    {
        public MovingId(Guid value) : base(value)
        {
        }
    }
}
