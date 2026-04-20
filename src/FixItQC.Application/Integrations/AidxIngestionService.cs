using System.Xml.Linq;

namespace FixItQC.Application.Integrations;

public sealed class AidxIngestionService
{
    public RawIntegrationEnvelope ParseEnvelope(string xml, string sourceMessageId, string? idempotencyKey)
    {
        var doc = XDocument.Parse(xml);
        var airline = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "Airline")?.Value;

        return new RawIntegrationEnvelope(
            "AIDX",
            "FlightUpdate",
            sourceMessageId,
            idempotencyKey,
            xml,
            airline);
    }
}
