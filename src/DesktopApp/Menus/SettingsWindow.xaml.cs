using Core.Interfaces;
using Core.Models.Configuration;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace DesktopApp
{
    public partial class SettingsWindow : Window
    {
        private readonly ILogger _logger;
        private readonly ISettingsService _settingsService;

        public SettingsWindow(ILogger logger, ISettingsService settingsService)
        {
            _logger = logger;
            _settingsService = settingsService;
            InitializeComponent();

            var settings = _settingsService.GetSettings();
            var minChannel = settings.MidiChannel.MinAllowedMidiChannel;
            var maxChannel = settings.MidiChannel.MaxAllowedMidiChannel;

            for (int i = minChannel; i <= maxChannel; i++)
                MidiChannelComboBox.Items.Add(i);

            MidiChannelComboBox.SelectedItem = _settingsService.GetSettings().MidiChannel?.Value;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (MidiChannelComboBox.SelectedItem is int channel)
            {
                _logger.LogInformation("Saving settings with MIDI channel: {Channel}", channel);
                var currentSettings = _settingsService.GetSettings();
                _settingsService.UpdateSettings(AppSettingsDataFactory.CreateFromExisting(currentSettings, channel));
            }
                

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
