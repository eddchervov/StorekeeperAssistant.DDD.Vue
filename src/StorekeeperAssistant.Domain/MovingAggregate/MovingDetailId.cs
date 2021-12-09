using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.MovingAggregate
{
    public class MovingDetailId : EntityId
    {
        public MovingDetailId(Guid value) : base(value)
        {
        }
    }
}
