using Core;
using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopApp;

public static class ServiceProviderHelper
{
    public static ServiceProvider RegisterAndCreate()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<IMidiDeviceService, MidiDeviceService>();
        services.AddSingleton<HxStompController>();
        services.AddSingleton<MainWindow>();

        return services.BuildServiceProvider();
    }
}