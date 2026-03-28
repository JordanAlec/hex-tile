namespace Core.Models.Configuration;

public class AppSettingsData
{
    public MidiChannelSettingsData MidiChannel { get; set; } = new();
    public KeyboardShortcutsSettingsData KeyboardShortcuts { get; set; } = new();
}
