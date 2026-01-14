using Core;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace DesktopApp.Screens
{
    public partial class FootswitchScreen : BaseScreen
    {
        public FootswitchScreen(ILogger<FootswitchScreen> logger, HxStompController controller) : base(logger, controller)
        {
            InitializeComponent();
        }

        private async void FS1_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.FS1, sender, e);

        private async void FS2_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.FS2, sender, e);

        private async void FS3_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.FS3, sender, e);

        private async void FS4_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.FS4, sender, e);

        private async void FS5_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.FS5, sender, e);

        private async void FS6_Click(object sender, RoutedEventArgs e)
        {
            Logger.LogWarning("FS6 button clicked. This only works with HX Stomp XL Units");
            await TileButton_Click(Controller.FS6, sender, e);
        }

        private async void FS7_Click(object sender, RoutedEventArgs e)
        {
            Logger.LogWarning("FS7 button clicked. This only works with HX Stomp XL Units");
            await TileButton_Click(Controller.FS7, sender, e);
        }

        private async void FS8_Click(object sender, RoutedEventArgs e)
        {
            Logger.LogWarning("FS8 button clicked. This only works with HX Stomp XL Units");
            await TileButton_Click(Controller.FS8, sender, e);
        }
    }
}
