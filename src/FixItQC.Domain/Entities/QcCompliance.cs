using FixItQC.Domain.Common;
using FixItQC.Domain.Enums;

namespace FixItQC.Domain.Entities;

public sealed class InspectionTemplate : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool IsAtaAligned { get; set; } = true;
    public bool IsJigTemplate { get; set; }
    public InspectionCadence Cadence { get; set; } = InspectionCadence.Monthly;
    public ICollection<InspectionTemplateSection> Sections { get; set; } = new List<InspectionTemplateSection>();
}

public sealed class InspectionTemplateSection : BaseEntity
{
    public Guid TemplateId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public ICollection<InspectionTemplateItem> Items { get; set; } = new List<InspectionTemplateItem>();
}

public sealed class InspectionTemplateItem : BaseEntity
{
    public Guid SectionId { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public bool Required { get; set; } = true;
}

public sealed class InspectionExecution : BaseEntity
{
    public Guid EquipmentId { get; set; }
    public Guid TemplateId { get; set; }
    public Guid ExecutedByUserId { get; set; }
    public bool Passed { get; set; }
    public bool RequiresSignoff { get; set; } = true;
    public string? SignoffBy { get; set; }
    public string? FailureSummary { get; set; }
}

public sealed class WorkOrder : BaseEntity
{
    public Guid StationId { get; set; }
    public Guid EquipmentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Priority { get; set; } = "Normal";
    public string Status { get; set; } = "Open";
    public Guid? CreatedFromInspectionExecutionId { get; set; }
}
