using Core;
using Core.Models.Requests;
using Core.Models.Responses;
using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopApp
{
    public partial class MainWindow : Window
    {
        private readonly HxStompController _controller;

        public MainWindow(HxStompController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutWindow
            {
                Owner = this
            };
            about.ShowDialog();
        }

        private async void TileButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            if (!button.IsHitTestVisible) return;

            button.IsHitTestVisible = false;

            
            var icon = GetIconFrom(button);
            var originalBackground = button.Background;
            var originalIcon = icon?.Kind ?? PackIconMaterialKind.None;

            var response = await RunControllerTask(button.Tag?.ToString());
            
            if (response.Success)
                SetSuccess(button, icon);
            else
                SetFailure(button, icon, response.Message ?? "An error occurred");

            await Task.Delay(1000);
            SetDefault(button, icon, originalBackground, originalIcon);
        }

        private void SetSuccess(Button button, PackIconMaterial? icon)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;
            button.Background = Brushes.LightGreen;
            
            if (icon == null) return;
            icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            icon.Foreground = Brushes.DarkGreen;

        }

        private void SetFailure(Button button, PackIconMaterial? icon, string failureMessage)
        {
            ErrorTextBlock.Text = failureMessage;
            ErrorTextBlock.Visibility = Visibility.Visible;
            button.Background = Brushes.IndianRed;
            
            if (icon == null) return;
            icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            icon.Foreground = Brushes.DarkRed;
        }

        private void SetDefault(Button button, PackIconMaterial? icon, Brush originalBackground, PackIconMaterialKind originalIcon)
        {
            button.Background = originalBackground;

            if (icon == null) return;
            icon.Kind = originalIcon;
            icon.Foreground = Brushes.Black;

            button.IsHitTestVisible = true;

        }

        private PackIconMaterial? GetIconFrom(Button button)
        {
            if (button.Content is not StackPanel stack || stack.Children.Count == 0) return null;
            return stack!.Children[0] as PackIconMaterial;
        }

        private async Task<SendMidiCommandResponse> RunControllerTask(string? taskName)
        {
            await Task.Delay(500);
            return taskName switch
            {
                "ToggleTuner" => _controller.ToggleTuner(),
                "PreviousPreset" => _controller.PreviousPreset(),
                "NextPreset" => _controller.NextPreset(),
                _ => new SendMidiCommandResponse(new SendMidiCommandRequest(0, 0, 0), false, "Unknown Task")
            };
        }
    }
}