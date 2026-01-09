namespace Core.Models.Requests;

public record SendMidiCommandRequest(int Controller, int Value, int Channel);