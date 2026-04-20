namespace FixItQC.Application.Comms;

public sealed record RadioTransmissionRequest(Guid SenderUserId, string ChannelScope, Guid ScopeId, bool IsVoice, string Payload);
public sealed record RadioTransmissionResult(string Transcript, string TtsText, bool Delivered);

public sealed class RadioCommsService
{
    public RadioTransmissionResult Process(RadioTransmissionRequest request)
    {
        var transcript = request.IsVoice
            ? $"[voice-transcribed] {request.Payload}" 
            : request.Payload;

        var ttsText = request.IsVoice
            ? transcript
            : $"Dispatch message: {request.Payload}";

        return new RadioTransmissionResult(transcript, ttsText, true);
    }
}
