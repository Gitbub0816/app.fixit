using FixItQC.Application.DTOs;

namespace FixItQC.Application.Services;

public sealed class FuelingWorkflowService
{
    public FuelingValidationResult ValidateAndScore(FuelingTicketDraft draft, decimal plannedGallons)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(draft.FlightCode)) errors.Add("Flight is required.");
        if (draft.TailNumber.Length is < 2 or > 4) errors.Add("Tail/ship number must be 2-4 characters.");
        if (draft.ArrivalByTank.Count == 0) errors.Add("Arrival fuel by tank is required.");
        if (draft.StartTotalizer <= 0) errors.Add("Start totalizer is required.");
        if (draft.DeliveredGallons <= 0) errors.Add("Fuel delivered must be greater than zero.");

        var maxImbalance = IsWideBody(draft.AircraftType) ? 1500m : 1000m;
        var wingTanks = draft.ArrivalByTank.Where(t => t.TankName.Contains("left", StringComparison.OrdinalIgnoreCase)
                                                    || t.TankName.Contains("right", StringComparison.OrdinalIgnoreCase)).ToList();
        if (wingTanks.Count >= 2)
        {
            var left = wingTanks.FirstOrDefault(t => t.TankName.Contains("left", StringComparison.OrdinalIgnoreCase))?.Pounds ?? 0;
            var right = wingTanks.FirstOrDefault(t => t.TankName.Contains("right", StringComparison.OrdinalIgnoreCase))?.Pounds ?? 0;
            if (Math.Abs(left - right) > maxImbalance)
                errors.Add($"Wing imbalance exceeds {maxImbalance} lbs.");
        }

        var variancePercent = plannedGallons == 0 ? 0 : Math.Abs((draft.DeliveredGallons - plannedGallons) / plannedGallons) * 100m;
        if (variancePercent > 10m)
            errors.Add("Fuel variance exceeds +/-10% threshold.");

        var onTimeStatus = ComputeOnTime(draft.ScheduledDepartureUtc, draft.CompletedUtc);
        return new FuelingValidationResult(errors.Count == 0, errors, onTimeStatus, Math.Round(variancePercent, 2));
    }

    public static string ComputeOnTime(DateTimeOffset departureUtc, DateTimeOffset completeUtc)
    {
        var diff = departureUtc - completeUtc;
        if (diff.TotalMinutes >= 5) return "ON_TIME";
        if (diff.TotalMinutes >= 0) return "AT_RISK";
        return "DELAY";
    }

    private static bool IsWideBody(string aircraftType)
    {
        var wideBodyHints = new[] { "330", "350", "777", "787", "747", "767" };
        return wideBodyHints.Any(aircraftType.Contains);
    }
}
