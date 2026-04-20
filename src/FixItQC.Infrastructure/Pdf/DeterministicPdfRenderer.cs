using System.Text;
using FixItQC.Application.Abstractions;
using FixItQC.Application.Pdf;

namespace FixItQC.Infrastructure.Pdf;

public sealed class DeterministicPdfRenderer : IPdfReportRenderer
{
    private readonly DeterministicPaginator _paginator = new();

    public byte[] RenderWorkOrderPdf(object model) => Render("WORK ORDER", model, BuildOperationalBlocks());
    public byte[] RenderAuditPdf(object model) => Render("AUDIT", model, BuildOperationalBlocks());
    public byte[] RenderDamagePdf(object model) => Render("DAMAGE", model, BuildOperationalBlocks());

    private byte[] Render(string reportType, object model, IReadOnlyList<PdfLayoutBlock> blocks)
    {
        var pages = _paginator.Paginate(blocks, 100);
        var sb = new StringBuilder();
        sb.AppendLine("%PDF-FAKE");
        sb.AppendLine($"Type={reportType}");
        sb.AppendLine($"Model={model}");
        sb.AppendLine($"Pages={pages.Count}");

        for (var i = 0; i < pages.Count; i++)
        {
            sb.AppendLine($"Page[{i + 1}] Header=FixItQC {reportType}");
            foreach (var block in pages[i].Blocks)
            {
                sb.AppendLine($"  Block={block.Title};Height={block.Height};KeepTogether={block.KeepTogether}");
            }
            sb.AppendLine("PageFooter=Confidential/FixItQC");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private static IReadOnlyList<PdfLayoutBlock> BuildOperationalBlocks() =>
    [
        new("Summary", 20),
        new("Checklist Results", 40),
        new("Image Grid", 30),
        new("Signoff", 15)
    ];
}
