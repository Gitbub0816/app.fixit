using FixItQC.Domain.Enums;

namespace FixItQC.Infrastructure.Seed;

public static class DevSeedData
{
    public static readonly IReadOnlyList<(string Email, string Password, PlatformRole Role, string Scope)> Logins =
    [
        ("global.support@fixitqc.local", "FixIt!Dev123", PlatformRole.GlobalAdmin, "FixIt QC platform"),
        ("orgadmin.east@demo.local", "FixIt!Dev123", PlatformRole.OrganizationalAdmin, "AeroFuel East org"),
        ("regional.bna@demo.local", "FixIt!Dev123", PlatformRole.RegionalAdmin, "BNA region"),
        ("station.bna@demo.local", "FixIt!Dev123", PlatformRole.StationAdmin, "BNA station"),
        ("dispatch.bna@demo.local", "FixIt!Dev123", PlatformRole.Dispatcher, "BNA station"),
        ("tech.bna@demo.local", "FixIt!Dev123", PlatformRole.Technician, "BNA station"),
        ("operator.bna01@demo.local", "FixIt!Dev123", PlatformRole.Operator, "BNA station")
    ];
}
