using BuildingBlocks.Domain;
using System;

namespace StorekeeperAssistant.Domain.Movings.MovingDetails;

public sealed class MovingDetailId : EntityId
{
    public MovingDetailId(Guid value) : base(value)
    {
    }
}
