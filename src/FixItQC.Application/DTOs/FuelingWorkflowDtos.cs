namespace FixItQC.Application.DTOs;

public sealed record FuelTankInput(string TankName, decimal Pounds);

public sealed record FuelingTicketDraft(
    string FlightCode,
    string AircraftType,
    string TailNumber,
    IReadOnlyList<FuelTankInput> ArrivalByTank,
    decimal StartTotalizer,
    decimal DeliveredGallons,
    decimal FuelDensityLbsPerGallon,
    DateTimeOffset ScheduledDepartureUtc,
    DateTimeOffset CompletedUtc);

public sealed record FuelingValidationResult(bool IsValid, IReadOnlyList<string> Errors, string OnTimeStatus, decimal VariancePercent);
