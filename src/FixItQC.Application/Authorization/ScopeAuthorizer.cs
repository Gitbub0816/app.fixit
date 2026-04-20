using FixItQC.Domain.Enums;

namespace FixItQC.Application.Authorization;

public interface IScopeAuthorizer
{
    bool CanManageUser(ScopeContext actor, ScopeContext target);
}

public sealed class ScopeAuthorizer : IScopeAuthorizer
{
    public bool CanManageUser(ScopeContext actor, ScopeContext target)
    {
        if (actor.Role == PlatformRole.GlobalAdmin)
        {
            return target.Role is PlatformRole.GlobalAdmin or PlatformRole.OrganizationalAdmin;
        }

        if (actor.Role == PlatformRole.OrganizationalAdmin)
        {
            return actor.OrganizationId == target.OrganizationId;
        }

        if (actor.Role == PlatformRole.RegionalAdmin)
        {
            return actor.OrganizationId == target.OrganizationId && actor.RegionId == target.RegionId;
        }

        if (actor.Role == PlatformRole.StationAdmin)
        {
            return actor.OrganizationId == target.OrganizationId
                && actor.RegionId == target.RegionId
                && actor.StationId == target.StationId;
        }

        return false;
    }
}
