namespace Core.Models.Configuration
{
    public static class AppSettingsDataFactory
    {
        public static AppSettingsData CreateFromExisting(AppSettingsData appSettingsData, int newChannel)
        {
            return new AppSettingsData
            {
                MidiChannel = new MidiChannelSettingsData
                {
                    Value = newChannel,
                    MinAllowedMidiChannel = appSettingsData.MidiChannel.MinAllowedMidiChannel,
                    MaxAllowedMidiChannel = appSettingsData.MidiChannel.MaxAllowedMidiChannel
                },
                KeyboardShortcuts = appSettingsData.KeyboardShortcuts
            };
        }

        public static AppSettingsData CreateWithShortcuts(AppSettingsData appSettingsData, KeyboardShortcutsSettingsData shortcuts)
        {
            return new AppSettingsData
            {
                MidiChannel = appSettingsData.MidiChannel,
                KeyboardShortcuts = shortcuts
            };
        }
    }
}
