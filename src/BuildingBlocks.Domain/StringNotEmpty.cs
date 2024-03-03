using System;
using System.Collections.Generic;

namespace BuildingBlocks.Domain;

public abstract class StringNotEmpty : ValueObject
{
    public string Value { get; } = null!;

    protected StringNotEmpty()
    {
    }

    public StringNotEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), $"{GetType()} must have a not empty string value");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
