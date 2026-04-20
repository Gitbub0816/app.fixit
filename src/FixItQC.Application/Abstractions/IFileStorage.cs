namespace FixItQC.Application.Abstractions;

public interface IFileStorage
{
    Task<string> SaveAsync(string container, string fileName, Stream content, CancellationToken ct = default);
}
