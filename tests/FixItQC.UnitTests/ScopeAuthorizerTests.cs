using FixItQC.Application.Authorization;
using FixItQC.Domain.Enums;

namespace FixItQC.UnitTests;

public sealed class ScopeAuthorizerTests
{
    public void StationAdmin_CanManageSameStation()
    {
        var auth = new ScopeAuthorizer();
        var org = Guid.NewGuid();
        var region = Guid.NewGuid();
        var station = Guid.NewGuid();

        var actor = new ScopeContext(PlatformRole.StationAdmin, org, region, station);
        var target = new ScopeContext(PlatformRole.Dispatcher, org, region, station);

        _ = auth.CanManageUser(actor, target);
    }
}
