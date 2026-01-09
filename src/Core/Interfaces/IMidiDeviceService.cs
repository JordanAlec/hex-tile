using Core.Models.Requests;
using Core.Models.Responses;
using NAudio.Midi;

namespace Core.Interfaces
{
    public interface IMidiDeviceService
    {
        SendMidiCommandResponse SendMidiCommand(MidiOut midiOut, SendMidiCommandRequest request);
        MidiOut? Find(string productName);
    }
}
