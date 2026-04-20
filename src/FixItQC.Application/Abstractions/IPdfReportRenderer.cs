namespace FixItQC.Application.Abstractions;

public interface IPdfReportRenderer
{
    byte[] RenderWorkOrderPdf(object model);
    byte[] RenderAuditPdf(object model);
    byte[] RenderDamagePdf(object model);
}
