using System.Text.Json;

namespace FixItQC.Application.Integrations;

public sealed class MappingEngineService
{
    private static readonly Dictionary<string, string> KnownFields = new(StringComparer.OrdinalIgnoreCase)
    {
        ["flightNo"] = "FlightNumber",
        ["flt"] = "FlightNumber",
        ["tail"] = "TailNumber",
        ["gate"] = "Gate",
        ["aircraft"] = "AircraftType",
        ["sta"] = "ScheduledArrivalUtc",
        ["std"] = "ScheduledDepartureUtc",
        ["cancelled"] = "IsCancelled"
    };

    public IReadOnlyList<MappingSuggestion> SuggestMappings(string jsonPayload)
    {
        using var doc = JsonDocument.Parse(jsonPayload);
        if (doc.RootElement.ValueKind != JsonValueKind.Object) return [];

        var suggestions = new List<MappingSuggestion>();
        foreach (var prop in doc.RootElement.EnumerateObject())
        {
            if (KnownFields.TryGetValue(prop.Name, out var target))
            {
                suggestions.Add(new MappingSuggestion(prop.Name, target, 0.95m));
            }
            else
            {
                var heuristic = KnownFields.Keys.FirstOrDefault(k => prop.Name.Contains(k, StringComparison.OrdinalIgnoreCase));
                if (heuristic is not null)
                    suggestions.Add(new MappingSuggestion(prop.Name, KnownFields[heuristic], 0.65m));
            }
        }

        return suggestions;
    }

    public NormalizedFlightPatch Normalize(Dictionary<string, object?> mappedFields, string airlineCode)
    {
        DateTimeOffset? ParseDate(string key)
            => mappedFields.TryGetValue(key, out var val) && val is not null
                ? DateTimeOffset.Parse(val.ToString()!).ToUniversalTime()
                : null;

        return new NormalizedFlightPatch(
            airlineCode,
            mappedFields.GetValueOrDefault("FlightNumber")?.ToString() ?? "UNKNOWN",
            mappedFields.GetValueOrDefault("TailNumber")?.ToString(),
            mappedFields.GetValueOrDefault("Gate")?.ToString(),
            mappedFields.GetValueOrDefault("AircraftType")?.ToString(),
            ParseDate("ScheduledArrivalUtc"),
            ParseDate("ScheduledDepartureUtc"),
            mappedFields.GetValueOrDefault("IsCancelled") is bool b ? b : null);
    }
}
