using Core.Interfaces;
using Core.Models.Configuration;
using System.Text.Json;

namespace Core.Services;

public class SettingsService : ISettingsService
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "HexTile", "settings.json");

    private AppSettingsData _data;

    public SettingsService()
    {
        _data = Load();
    }

    public AppSettingsData GetSettings() => _data;

    public void UpdateSettings(AppSettingsData settings)
    {
        var midiChannelValue = Math.Clamp(settings.MidiChannel.Value, settings.MidiChannel.MinAllowedMidiChannel, settings.MidiChannel.MaxAllowedMidiChannel);
        _data = AppSettingsDataFactory.CreateFromExisting(settings, midiChannelValue);
        Save();
    }

    private AppSettingsData Load()
    {
        if (!File.Exists(SettingsPath))
            return Defaults.SettingsData.Default;

        try
        {
            var json = File.ReadAllText(SettingsPath);
            return JsonSerializer.Deserialize<AppSettingsData>(json) ?? Defaults.SettingsData.Default;
        }
        catch
        {
            return Defaults.SettingsData.Default;
        }
    }

    private void Save()
    {
        var dir = Path.GetDirectoryName(SettingsPath)!;
        Directory.CreateDirectory(dir);
        var json = JsonSerializer.Serialize(_data, Defaults.SerialisationOptions.Default);
        File.WriteAllText(SettingsPath, json);
    }
}
