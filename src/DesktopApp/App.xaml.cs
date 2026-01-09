using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopApp
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var serviceProvider = ServiceProviderHelper.RegisterAndCreate();

            // Resolve MainWindow and show
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
