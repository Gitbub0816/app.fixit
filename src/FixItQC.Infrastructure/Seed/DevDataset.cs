namespace FixItQC.Infrastructure.Seed;

public static class DevDataset
{
    public static object Organizations => new[]
    {
        new {
            Name = "AeroFuel East",
            Types = new[] { "FuelServiceProvider" },
            Regions = new[]
            {
                new { Name = "Southeast", Stations = new[] { "BNA", "CLT" } },
                new { Name = "Mid-Atlantic", Stations = new[] { "IAD", "RDU" } }
            }
        },
        new {
            Name = "SkyLink Airlines",
            Types = new[] { "Airline" },
            Regions = new[]
            {
                new { Name = "Central Ops", Stations = new[] { "DFW", "ORD" } }
            }
        },
        new {
            Name = "Harbor Fuel Terminal",
            Types = new[] { "FuelStorageFacility", "FuelServiceProvider" },
            Regions = new[]
            {
                new { Name = "Storage West", Stations = new[] { "SFO-TANK", "OAK-TANK" } }
            }
        }
    };
}
