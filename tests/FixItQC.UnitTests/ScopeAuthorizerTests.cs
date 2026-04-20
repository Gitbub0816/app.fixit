using FixItQC.Application.Authorization;
using FixItQC.Domain.Enums;
using Xunit;

namespace FixItQC.UnitTests;

public sealed class ScopeAuthorizerTests
{
    [Fact]
    public void StationAdmin_CanManageSameStation()
    {
        var auth = new ScopeAuthorizer();
        var org = Guid.NewGuid();
        var region = Guid.NewGuid();
        var station = Guid.NewGuid();

        var actor = new ScopeContext(PlatformRole.StationAdmin, org, region, station);
        var target = new ScopeContext(PlatformRole.Dispatcher, org, region, station);

        var result = auth.CanManageUser(actor, target);
        Assert.True(result);
    }
}
