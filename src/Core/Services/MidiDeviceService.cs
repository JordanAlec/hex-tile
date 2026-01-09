using Core.Interfaces;
using Core.Models.Requests;
using Core.Models.Responses;
using NAudio.Midi;

namespace Core.Services;

public class MidiDeviceService : IMidiDeviceService
{
    public SendMidiCommandResponse SendMidiCommand(MidiOut midiOut, SendMidiCommandRequest request)
    {
        try
        {
            midiOut.Send(MidiMessage.ChangeControl(request.Controller, request.Value, request.Channel).RawData);
            return new SendMidiCommandResponse(request, true);
        }
        catch (Exception ex)
        {
            return new SendMidiCommandResponse(request, false, ex.Message);
        }
    }

    public MidiOut? Find(string productName)
    {
        for (var device = 0; device < MidiOut.NumberOfDevices; device++)
        {
            var midiOut = MidiOut.DeviceInfo(device);
            if (midiOut.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase))
            {
                return new MidiOut(device);
            }
        }

        return null;
    }
}