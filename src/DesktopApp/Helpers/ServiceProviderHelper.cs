using Core;
using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DesktopApp.Helpers;

public static class ServiceProviderHelper
{
    public static ServiceProvider RegisterAndCreate()
    {
        var services = new ServiceCollection();

        services.AddLogging(options =>
        {
            options.AddSerilog(new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File($"logs\\hex_tile_log_.log", rollingInterval: RollingInterval.Day)
                .CreateLogger());
        });
        services.AddSingleton<IMidiDeviceService, MidiDeviceService>();
        services.AddSingleton<HxStompController>();
        services.AddSingleton<MainWindow>();

        return services.BuildServiceProvider();
    }
}