using Core;
using Core.Factories;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopApp
{
    public partial class MainWindow : Window
    {
        private Screens.NavigationScreen _navigationScreen;
        private Screens.FootswitchScreen _footswitchScreen;
        private Screens.SnapshotScreen _snapshotScreen;

        private readonly ILogger<MainWindow> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ISettingsService _settingsService;
        private readonly HxStompController _controller;
        private bool _dialogOpen;

        public MainWindow(ILogger<MainWindow> logger, HxStompController controller, ILoggerFactory loggerFactory, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _controller = controller;
            _logger = logger;
            _loggerFactory = loggerFactory;
            InitializeComponent();

            _logger.LogInformation("HexTile application started");

            _navigationScreen = new Screens.NavigationScreen(loggerFactory.CreateLogger<Screens.NavigationScreen>(), controller);
            _footswitchScreen = new Screens.FootswitchScreen(loggerFactory.CreateLogger<Screens.FootswitchScreen>(), controller);
            _snapshotScreen = new Screens.SnapshotScreen(loggerFactory.CreateLogger<Screens.SnapshotScreen>(), controller);

            _navigationScreen.ErrorOccurred += OnErrorOccurred;
            _footswitchScreen.ErrorOccurred += OnErrorOccurred;
            _snapshotScreen.ErrorOccurred += OnErrorOccurred;

            NavigationScreenControl.Content = _navigationScreen;
            FootswitchScreenControl.Content = _footswitchScreen;
            SnapshotScreenControl.Content = _snapshotScreen;

            // Set initial active button style
            NavigationButton.Style = (Style)FindResource("ActiveTabButtonStyle");

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            _logger.LogInformation("HexTile application shutting down");
        }

        private async void OnErrorOccurred(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                ErrorTextBlock.Visibility = Visibility.Collapsed;
                ErrorTextBlock.Text = string.Empty;
            }
            else
            {
                ErrorTextBlock.Text = errorMessage;
                ErrorTextBlock.Visibility = Visibility.Visible;
                
                await Task.Delay(Defaults.Window.ErrorVisibilityMessageMilliseconds);
                ErrorTextBlock.Visibility = Visibility.Collapsed;
                ErrorTextBlock.Text = string.Empty;
            }
        }

        private void AlwaysOnTop_Click(object sender, RoutedEventArgs e)
        {
            Topmost = (sender as MenuItem)?.IsChecked ?? false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            _dialogOpen = true;
            new SettingsWindow(_loggerFactory.CreateLogger<SettingsWindow>(), _settingsService) { Owner = this }.ShowDialog();
            _dialogOpen = false;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            _dialogOpen = true;
            new AboutWindow() { Owner = this }.ShowDialog();
            _dialogOpen = false;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_dialogOpen) return;

            var map = KeyboardShortcutMapFactory.Create(_settingsService.GetSettings().KeyboardShortcuts, _controller);

            if (!map.TryGetValue(e.Key.ToString(), out var action)) return;

            e.Handled = true;
            var response = action();
            OnErrorOccurred(response.Success ? string.Empty : response.Message ?? "An error occurred");
        }

        private void NavigationScreen_Click(object sender, RoutedEventArgs e)
        {
            NavigationScreenControl.Visibility = Visibility.Visible;
            FootswitchScreenControl.Visibility = Visibility.Collapsed;
            SnapshotScreenControl.Visibility = Visibility.Collapsed;
            
            NavigationButton.Style = (Style)FindResource("ActiveTabButtonStyle");
            FootswitchButton.Style = (Style)FindResource("TabButtonStyle");
            SnapshotButton.Style = (Style)FindResource("TabButtonStyle");
        }

        private void FootswitchScreen_Click(object sender, RoutedEventArgs e)
        {
            NavigationScreenControl.Visibility = Visibility.Collapsed;
            FootswitchScreenControl.Visibility = Visibility.Visible;
            SnapshotScreenControl.Visibility = Visibility.Collapsed;
            
            NavigationButton.Style = (Style)FindResource("TabButtonStyle");
            FootswitchButton.Style = (Style)FindResource("ActiveTabButtonStyle");
            SnapshotButton.Style = (Style)FindResource("TabButtonStyle");
        }

        private void SnapshotScreen_Click(object sender, RoutedEventArgs e)
        {
            NavigationScreenControl.Visibility = Visibility.Collapsed;
            FootswitchScreenControl.Visibility = Visibility.Collapsed;
            SnapshotScreenControl.Visibility = Visibility.Visible;
            
            NavigationButton.Style = (Style)FindResource("TabButtonStyle");
            FootswitchButton.Style = (Style)FindResource("TabButtonStyle");
            SnapshotButton.Style = (Style)FindResource("ActiveTabButtonStyle");
        }
    }
}