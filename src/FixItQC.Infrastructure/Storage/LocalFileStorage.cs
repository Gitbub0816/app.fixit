using FixItQC.Application.Abstractions;

namespace FixItQC.Infrastructure.Storage;

public sealed class LocalFileStorage : IFileStorage
{
    private readonly string _basePath;

    public LocalFileStorage(string? basePath = null)
    {
        _basePath = basePath ?? Path.Combine(AppContext.BaseDirectory, "storage");
    }

    public async Task<string> SaveAsync(string container, string fileName, Stream content, CancellationToken ct = default)
    {
        var dir = Path.Combine(_basePath, container);
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, $"{Guid.NewGuid():N}-{fileName}");

        await using var file = File.Create(path);
        await content.CopyToAsync(file, ct);

        return path;
    }
}
