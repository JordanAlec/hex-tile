using Core;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace DesktopApp
{
    public partial class MainWindow : Window
    {
        private Screens.NavigationScreen? _navigationScreen;
        private Screens.FootswitchScreen? _footswitchScreen;

        public MainWindow(ILogger<MainWindow> logger, HxStompController controller, ILoggerFactory loggerFactory)
        {
            InitializeComponent();

            var navLogger = loggerFactory.CreateLogger<Screens.NavigationScreen>();
            var fsLogger = loggerFactory.CreateLogger<Screens.FootswitchScreen>();

            _navigationScreen = new Screens.NavigationScreen(navLogger, controller);
            _footswitchScreen = new Screens.FootswitchScreen(fsLogger, controller);

            _navigationScreen.ErrorOccurred += OnErrorOccurred;
            _footswitchScreen.ErrorOccurred += OnErrorOccurred;

            NavigationScreenControl.Content = _navigationScreen;
            FootswitchScreenControl.Content = _footswitchScreen;
        }

        private void OnErrorOccurred(string errorMessage)
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
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutWindow { Owner = this };
            about.ShowDialog();
        }

        private void ToggleScreen_Click(object sender, RoutedEventArgs e)
        {

            if (NavigationScreenControl.Visibility == Visibility.Visible)
            {
                NavigationScreenControl.Visibility = Visibility.Collapsed;
                FootswitchScreenControl.Visibility = Visibility.Visible;
                ToggleScreenText.Text = "Show Navigation";
            }
            else
            {
                NavigationScreenControl.Visibility = Visibility.Visible;
                FootswitchScreenControl.Visibility = Visibility.Collapsed;
                ToggleScreenText.Text = "Show Footswitches";
            }
        }
    }
}