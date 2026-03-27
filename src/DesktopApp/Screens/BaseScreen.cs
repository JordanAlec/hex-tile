using Core;
using Core.Models.Responses;
using MahApps.Metro.IconPacks;
using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopApp.Screens
{
    public abstract class BaseScreen : UserControl
    {
        protected readonly ILogger Logger;
        protected readonly HxStompController Controller;
        public event Action<string>? ErrorOccurred;

        protected BaseScreen(ILogger logger, HxStompController controller)
        {
            Logger = logger;
            Controller = controller;
        }

        protected async Task TileButton_Click(Func<SendMidiCommandResponse> sendMidiFunc, object sender, RoutedEventArgs e)
        {
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

            await Task.Delay(Defaults.Button.ResetDelayMilliseconds);
            SetDefault(button, icon, originalBackground, originalIcon);
        }

        private void SetSuccess(Button button, PackIconMaterial? icon)
        {
            ErrorOccurred?.Invoke(string.Empty);
            button.Background = new SolidColorBrush(Color.FromRgb(0x2E, 0x7D, 0x32)); // Dark green for success

            if (icon == null) return;
            icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            icon.Foreground = new SolidColorBrush(Color.FromRgb(0x66, 0xBB, 0x6A)); // Light green for icon
        }

        private void SetFailure(Button button, PackIconMaterial? icon, string failureMessage)
        {
            Logger.LogError("MIDI Command Failure: {Message}", failureMessage);
            ErrorOccurred?.Invoke(failureMessage);
            button.Background = new SolidColorBrush(Color.FromRgb(0xE6, 0x39, 0x46)); // AccentRed

            if (icon == null) return;
            icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            icon.Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF)); // White for visibility
        }

        private void SetDefault(Button button, PackIconMaterial? icon, Brush originalBackground, PackIconMaterialKind originalIcon)
        {
            button.Background = originalBackground;

            if (icon == null) return;
            icon.Kind = originalIcon;
            icon.Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF)); // White - TextPrimary

            button.IsHitTestVisible = true;
        }

        private PackIconMaterial? GetIconFrom(Button button)
        {
            if (button.Content is not StackPanel stack || stack.Children.Count == 0) return null;
            return stack!.Children[0] as PackIconMaterial;
        }
    }
}
