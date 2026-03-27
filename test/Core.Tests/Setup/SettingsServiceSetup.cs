using Core;
using Core.Interfaces;
using NSubstitute;

namespace Core.Tests.Setup;

public static class SettingsServiceSetup
{
    public static void SetupGetSettings(this ISettingsService settingService)
    {
        settingService.GetSettings().Returns(Defaults.SettingsData.Default);
    }
}