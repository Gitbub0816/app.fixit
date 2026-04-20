namespace FixItQC.Application.Compliance;

public sealed class JigComplianceService
{
    public bool ShouldLoadJigTemplates(bool stationJigEnabled) => stationJigEnabled;

    public string BuildDashboardLabel(bool stationJigEnabled)
        => stationJigEnabled ? "JIG Compliance Enabled" : "JIG Compliance Disabled";
}
