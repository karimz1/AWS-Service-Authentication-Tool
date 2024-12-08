using System.ComponentModel;
using System.Reflection;

namespace AwsServiceAuthenticator.Core.Utls;

public static class EnumExtensions
{
    /// <summary>
    /// Gets a description for an enum value.
    /// </summary>
    private static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// Generates a list of all available enum values with their descriptions.
    /// </summary>
    public static string GetAvailableValuesWithDescriptions<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T)).Cast<T>();

        return string.Join(Environment.NewLine,
            values.Select(value =>
            {
                var description = value.GetDescription();
                return $"{value}: {description}";
            }));
    }

    /// <summary>
    /// Tries to parse a string into an enum value. Falls back to a default value if parsing fails.
    /// </summary>
    public static T TryParseOrFallback<T>(this string value, T fallback = default) where T : struct, Enum
    {
        return Enum.TryParse(value, true, out T result) ? result : fallback;
    }
}