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

    // The following commands work for both the HX Stomp and HX Stomp XL
    public SendMidiCommandResponse FS1() => SendCommand(49, 0);
    public SendMidiCommandResponse FS2() => SendCommand(50, 0);
    public SendMidiCommandResponse FS3() => SendCommand(51, 0);
    public SendMidiCommandResponse FS4() => SendCommand(52, 0);
    public SendMidiCommandResponse FS5() => SendCommand(53, 0);

    // The following commands only work for the HX Stomp XL
    public SendMidiCommandResponse FS6() => SendCommand(54, 0);
    public SendMidiCommandResponse FS7() => SendCommand(55, 0);
    public SendMidiCommandResponse FS8() => SendCommand(56, 0);
}