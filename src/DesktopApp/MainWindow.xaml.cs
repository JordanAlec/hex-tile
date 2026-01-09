using Core;
using Core.Models.Requests;
using Core.Models.Responses;
using MahApps.Metro.IconPacks;
using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopApp
{
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> _logger;
        private readonly HxStompController _controller;

        public MainWindow(ILogger<MainWindow> logger, HxStompController controller)
        {
            InitializeComponent();
            _logger = logger;
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

        private async Task TileButton_Click(Func<SendMidiCommandResponse> sendMidiFunc, object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);

            if (sender is not Button button) return;
            if (!button.IsHitTestVisible) return;

            button.IsHitTestVisible = false;

            
            var icon = GetIconFrom(button);
            var originalBackground = button.Background;
            var originalIcon = icon?.Kind ?? PackIconMaterialKind.None;

            var response = sendMidiFunc();
            
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
            _logger.LogError("MIDI Command Failure: {Message}", failureMessage);

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

        private async void ToggleTuner_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.ToggleTuner, sender, e);
        }

        private async void PreviousPreset_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.PreviousPreset, sender, e);
        }

        private async void NextPreset_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.NextPreset, sender, e);
        }

        private async void FS1_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.FS1, sender, e);
        }

        private async void FS2_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.FS2, sender, e);
        }

        private async void FS3_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.FS3, sender, e);
        }

        private async void FS4_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.FS4, sender, e);
        }

        private async void FS5_Click(object sender, RoutedEventArgs e)
        {
            await TileButton_Click(_controller.FS5, sender, e);
        }

        private async void FS6_Click(object sender, RoutedEventArgs e)
        {
            _logger.LogWarning("FS6 button clicked. This only works with HX Stomp XL Units");
            await TileButton_Click(_controller.FS6, sender, e);
        }

        private async void FS7_Click(object sender, RoutedEventArgs e)
        {
            _logger.LogWarning("FS7 button clicked. This only works with HX Stomp XL Units");
            await TileButton_Click(_controller.FS7, sender, e);
        }

        private async void FS8_Click(object sender, RoutedEventArgs e)
        {
            _logger.LogWarning("FS8 button clicked. This only works with HX Stomp XL Units");
            await TileButton_Click(_controller.FS8, sender, e);
        }
    }
}