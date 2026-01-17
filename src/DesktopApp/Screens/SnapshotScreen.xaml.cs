using Core;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace DesktopApp.Screens
{
    public partial class SnapshotScreen : BaseScreen
    {
        public SnapshotScreen(ILogger<SnapshotScreen> logger, HxStompController controller) : base(logger, controller)
        {
            InitializeComponent();
        }

        private async void Snapshot1_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.Snapshot1, sender, e);

        private async void Snapshot2_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.Snapshot2, sender, e);

        private async void Snapshot3_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.Snapshot3, sender, e);

        private async void Snapshot4_Click(object sender, RoutedEventArgs e)
        {
            Logger.LogWarning("Snapshot4 button clicked. This only works with HX Stomp XL Units");
            await TileButton_Click(Controller.Snapshot4, sender, e);
        }

        private async void NextSnapshot_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.NextSnapshot, sender, e);
        private async void PreviousSnapshot_Click(object sender, RoutedEventArgs e) => await TileButton_Click(Controller.PreviousSnapshot, sender, e);
    }
}
