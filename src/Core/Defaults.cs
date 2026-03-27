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
                }
            };
        }
    }
}
