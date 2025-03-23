using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BuildingBlocks.Domain;

public static class EnumExtensions
{
    /// <summary>
    /// Get description for an enum or <see cref="null"/>. Use reflection.
    /// </summary>
    /// <param name="enum">Enum</param>
    /// <returns>Returns description string or <see cref="null"/>.</returns>
    public static string? GetDescription(this Enum @enum)
    {
        return @enum
            .GetType()
            .GetMember(@enum.ToString())
            .FirstOrDefault()?
                .GetCustomAttribute<DescriptionAttribute>()?.Description;
    }
}
