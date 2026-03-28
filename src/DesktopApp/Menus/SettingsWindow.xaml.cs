using Core.Interfaces;
using Core.Models.Configuration;
using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            MidiChannelComboBox.SelectedItem = settings.MidiChannel?.Value;

            var shortcuts = settings.KeyboardShortcuts;
            ShortcutFS1.Text = shortcuts.FS1;
            ShortcutFS2.Text = shortcuts.FS2;
            ShortcutFS3.Text = shortcuts.FS3;
            ShortcutFS4.Text = shortcuts.FS4;
            ShortcutFS5.Text = shortcuts.FS5;
            ShortcutFS6.Text = shortcuts.FS6;
            ShortcutFS7.Text = shortcuts.FS7;
            ShortcutFS8.Text = shortcuts.FS8;
            ShortcutSnapshot1.Text = shortcuts.Snapshot1;
            ShortcutSnapshot2.Text = shortcuts.Snapshot2;
            ShortcutSnapshot3.Text = shortcuts.Snapshot3;
            ShortcutSnapshot4.Text = shortcuts.Snapshot4;
            ShortcutNextSnapshot.Text = shortcuts.NextSnapshot;
            ShortcutPreviousSnapshot.Text = shortcuts.PreviousSnapshot;
            ShortcutNextPreset.Text = shortcuts.NextPreset;
            ShortcutPreviousPreset.Text = shortcuts.PreviousPreset;
            ShortcutToggleTuner.Text = shortcuts.ToggleTuner;
        }

        private void ShortcutBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is not TextBox box) return;

            ShortcutErrorText.Visibility = Visibility.Collapsed;

            if (e.Key is Key.Back or Key.Delete)
            {
                e.Handled = true;
                box.Text = string.Empty;
                return;
            }

            if (IsModifierKey(e.Key)) return;

            e.Handled = true;
            box.Text = e.Key.ToString();
        }

        private static bool IsModifierKey(Key key) =>
            key is Key.LeftCtrl or Key.RightCtrl or
                   Key.LeftAlt or Key.RightAlt or
                   Key.LeftShift or Key.RightShift or
                   Key.LWin or Key.RWin or
                   Key.System or Key.None;

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var shortcuts = GetShortcutsFromBoxes();

            var seen = new HashSet<string>();
            var duplicates = shortcuts
                .Shortcuts.Where(k => !string.IsNullOrEmpty(k) && !seen.Add(k))
                .ToHashSet();

            if (duplicates.Any())
            {
                ShortcutErrorText.Text = $"Duplicate shortcuts detected: {string.Join(", ", duplicates)}";
                ShortcutErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (MidiChannelComboBox.SelectedItem is not int channel) return;

            var currentSettings = _settingsService.GetSettings();
            _logger.LogInformation("Saving settings with MIDI channel: {Channel}", channel);
            _settingsService.UpdateSettings(
                AppSettingsDataFactory.CreateWithShortcuts(
                    AppSettingsDataFactory.CreateFromExisting(currentSettings, channel),
                    shortcuts));

            Close();
        }

        private KeyboardShortcutsSettingsData GetShortcutsFromBoxes() => new()
        {
            FS1 = ShortcutFS1.Text,
            FS2 = ShortcutFS2.Text,
            FS3 = ShortcutFS3.Text,
            FS4 = ShortcutFS4.Text,
            FS5 = ShortcutFS5.Text,
            FS6 = ShortcutFS6.Text,
            FS7 = ShortcutFS7.Text,
            FS8 = ShortcutFS8.Text,
            Snapshot1 = ShortcutSnapshot1.Text,
            Snapshot2 = ShortcutSnapshot2.Text,
            Snapshot3 = ShortcutSnapshot3.Text,
            Snapshot4 = ShortcutSnapshot4.Text,
            NextSnapshot = ShortcutNextSnapshot.Text,
            PreviousSnapshot = ShortcutPreviousSnapshot.Text,
            NextPreset = ShortcutNextPreset.Text,
            PreviousPreset = ShortcutPreviousPreset.Text,
            ToggleTuner = ShortcutToggleTuner.Text
        };

        private void Cancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
