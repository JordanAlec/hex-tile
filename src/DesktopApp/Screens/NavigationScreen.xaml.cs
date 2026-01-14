using Core;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace DesktopApp.Screens
{
    public partial class NavigationScreen : BaseScreen
    {
        public NavigationScreen(ILogger<NavigationScreen> logger, HxStompController controller): base(logger, controller)
        {
            InitializeComponent();
        }

        private async void ToggleTuner_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.ToggleTuner, sender, e);

        private async void PreviousPreset_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.PreviousPreset, sender, e);

        private async void NextPreset_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.NextPreset, sender, e);
    }
}
