namespace FixItQC.Domain.Enums;

public enum InspectionCadence
{
    Daily,
    Weekly,
    Monthly,
    Quarterly,
    SemiAnnual,
    Annual
}

public enum InspectionWindowStatus
{
    Valid,
    Grace,
    NonCompliant
}

public enum OnTimeStatus
{
    Green,
    Yellow,
    Red
}

public enum DispatchViewMode
{
    VisualTimeline,
    DataTable
}

public enum DispatchServiceType
{
    Jet,
    Gse,
    Both
}

public enum ExtendedDispatchStatus
{
    Pending,
    Assigned,
    InProgress,
    Completed,
    AtRisk,
    Delayed,
    Cancelled,
    Exception
}
