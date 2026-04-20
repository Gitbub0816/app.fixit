using FixItQC.Domain.Enums;

namespace FixItQC.Application.Authorization;

public sealed record ScopeContext(
    PlatformRole Role,
    Guid? OrganizationId,
    Guid? RegionId,
    Guid? StationId);
