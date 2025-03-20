using System;

namespace BuildingBlocks.Domain;

public abstract class EntityId : IEquatable<EntityId>
{
    public Guid Value { get; }

    protected EntityId(Guid value)
    {
        if(value == Guid.Empty)
        {
            throw new ArgumentNullException("Id value cannot be default!");
        }

        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is EntityId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(EntityId? other)
    {
        if (other is null)
        {
            return false;
        }

        return Value == other.Value;
    }

    public static bool operator ==(EntityId? obj1, EntityId? obj2)
    {
        if (object.Equals(obj1, null))
        {
            if (object.Equals(obj2, null))
            {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(EntityId? x, EntityId? y)
    {
        return !(x == y);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
