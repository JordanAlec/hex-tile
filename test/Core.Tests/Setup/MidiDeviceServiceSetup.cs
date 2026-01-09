using Core.Interfaces;
using Core.Models.Requests;
using Core.Models.Responses;
using NAudio.Midi;
using NSubstitute;

namespace Core.Tests.Setup;

public static class MidiDeviceServiceSetup
{
    public static void SetupFind(this IMidiDeviceService midiService)
    {
        midiService.Find(Arg.Any<string>()).Returns(new MidiOut(0));
    }

    public static void SetupSuccessfulSendMidiCommand(this IMidiDeviceService midiService)
    {
        midiService.SendMidiCommand(Arg.Any<MidiOut>(), 
            Arg.Any<SendMidiCommandRequest>())
            .Returns(call => new SendMidiCommandResponse(call.Arg<SendMidiCommandRequest>(), true));
    }
}