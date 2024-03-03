using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.Movings;

public sealed class MovingId : EntityId
{
    public MovingId(Guid value) : base(value)
    {
    }
}
