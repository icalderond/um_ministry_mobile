using System.Reflection;
using System.Text.Json;

namespace UMMinistry.Core.Extensions;

public static class JsonExtensions
{
    /// <summary>
    /// Is valid json
    /// </summary>
    /// <param name="json"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsValidJson<T>(this string? json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            // Get all public properties of type T
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Check if all properties exist in the JSON
            foreach (var prop in props)
            {
                if (!root.TryGetProperty(prop.Name, out _))
                {
                    return false; // Missing a property
                }
            }

            return true; // All properties matched
        }
        catch
        {
            return false; // Invalid JSON
        }
    }
}