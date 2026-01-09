using Core.Models.Requests;

namespace Core.Models.Responses;

public record SendMidiCommandResponse
{
    public SendMidiCommandRequest Request { get; init; }
    public bool Success { get; init; }
    public string? Message { get; init; }

    public SendMidiCommandResponse(SendMidiCommandRequest request, bool success, string? message = null)
    {
        Request = request;
        Success = success;
        Message = message ?? string.Empty;
    }
};