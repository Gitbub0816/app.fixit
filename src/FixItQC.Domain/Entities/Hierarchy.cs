using FixItQC.Domain.Common;
using FixItQC.Domain.Enums;

namespace FixItQC.Domain.Entities;

public sealed class ClientOrganization : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public OrganizationType Types { get; set; }
    public ICollection<Region> Regions { get; set; } = new List<Region>();
}

public sealed class Region : BaseEntity
{
    public Guid OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Station> Stations { get; set; } = new List<Station>();
}

public sealed class Station : BaseEntity
{
    public Guid RegionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IataCode { get; set; } = string.Empty;
}
