namespace FixItQC.Application.Pdf;

public sealed record PdfLayoutBlock(string Title, int Height, bool KeepTogether = true);
public sealed record PdfPage(IReadOnlyList<PdfLayoutBlock> Blocks);

public sealed class DeterministicPaginator
{
    public IReadOnlyList<PdfPage> Paginate(IEnumerable<PdfLayoutBlock> blocks, int pageHeight)
    {
        var pages = new List<PdfPage>();
        var current = new List<PdfLayoutBlock>();
        var used = 0;

        foreach (var block in blocks)
        {
            if (used + block.Height > pageHeight && current.Count > 0)
            {
                pages.Add(new PdfPage(current.ToList()));
                current.Clear();
                used = 0;
            }

            current.Add(block);
            used += block.Height;
        }

        if (current.Count > 0) pages.Add(new PdfPage(current));
        return pages;
    }
}
