using System.Text;
using FixItQC.Application.Abstractions;

namespace FixItQC.Infrastructure.Pdf;

public sealed class DeterministicPdfRenderer : IPdfReportRenderer
{
    public byte[] RenderWorkOrderPdf(object model) => Render("WORK ORDER", model);
    public byte[] RenderAuditPdf(object model) => Render("AUDIT", model);
    public byte[] RenderDamagePdf(object model) => Render("DAMAGE", model);

    private static byte[] Render(string reportType, object model)
    {
        // Placeholder scaffolding where a deterministic layout PDF engine (QuestPDF/iText7)
        // can define explicit blocks, table pagination, repeated headers and intentional page breaks.
        return Encoding.UTF8.GetBytes($"%PDF-FAKE\nType={reportType}\nModel={model}\n");
    }
}
