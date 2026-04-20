using FixItQC.Domain.Enums;

namespace FixItQC.Application.Services;

public sealed class BulletinApprovalService
{
    public bool CanSubmitUpward(PlatformRole actorRole, PlatformRole targetApproverRole)
    {
        return (actorRole, targetApproverRole) switch
        {
            (PlatformRole.StationAdmin, PlatformRole.RegionalAdmin) => true,
            (PlatformRole.RegionalAdmin, PlatformRole.OrganizationalAdmin) => true,
            _ => false
        };
    }
}
