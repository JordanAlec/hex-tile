using Core.Interfaces;
using Core.Models.Requests;
using Core.Models.Responses;

namespace Core;

public class HxStompController
{
    private const int DefaultChannel = 1;
    private const string NotFoundError = "No HX Stomp found via USB. Please connect and restart the app.";

    private readonly IMidiDeviceService _midiDeviceService;

    public HxStompController(IMidiDeviceService midiDeviceService)
    {
        _midiDeviceService = midiDeviceService;
    }

    private SendMidiCommandResponse SendCommand(int controller, int value)
    {
        var request = new SendMidiCommandRequest(controller, value, DefaultChannel);
        using var hxStomp = _midiDeviceService.Find("HX Stomp");

        return hxStomp == null
            ? new SendMidiCommandResponse(request, false, NotFoundError)
            : _midiDeviceService.SendMidiCommand(hxStomp, request);
    }

    public SendMidiCommandResponse ToggleTuner() => SendCommand(68, 127);
    public SendMidiCommandResponse PreviousPreset() => SendCommand(72, 0);
    public SendMidiCommandResponse NextPreset() => SendCommand(72, 127);
}