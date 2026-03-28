using Core.Models.Configuration;
using System.Text.Json;

namespace Core
{
    public static class Defaults
    {
        public static class SerialisationOptions
        {
            public static JsonSerializerOptions Default => new()
            {
                WriteIndented = true,
            };
        }

        public static class SettingsData
        {
            public static AppSettingsData Default => new()
            {
                MidiChannel = new MidiChannelSettingsData
                {
                    Value = 1,
                    MinAllowedMidiChannel = 1,
                    MaxAllowedMidiChannel = 16
                },
                KeyboardShortcuts = new KeyboardShortcutsSettingsData
                {
                    FS1 = "D1",
                    FS2 = "D2",
                    FS3 = "D3",
                    FS4 = "D4",
                    FS5 = "D5",
                    FS6 = "D6",
                    FS7 = "D7",
                    FS8 = "D8",
                    Snapshot1 = "Q",
                    Snapshot2 = "W",
                    Snapshot3 = "E",
                    Snapshot4 = "R",
                    NextSnapshot = "Up",
                    PreviousSnapshot = "Down",
                    NextPreset = "Right",
                    PreviousPreset = "Left",
                    ToggleTuner = "T"
                }
            };
        }
    }
}
