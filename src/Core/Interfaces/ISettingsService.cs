using Core.Models.Configuration;

namespace Core.Interfaces;

public interface ISettingsService
{
    AppSettingsData GetSettings();
    void UpdateSettings(AppSettingsData settings);
}
