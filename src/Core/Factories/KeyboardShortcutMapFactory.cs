using Core.Models.Configuration;
using Core.Models.Responses;

namespace Core.Factories;

public static class KeyboardShortcutMapFactory
{
    public static Dictionary<string, Func<SendMidiCommandResponse>> Create(KeyboardShortcutsSettingsData shortcuts, HxStompController controller)
    {
        var map = new Dictionary<string, Func<SendMidiCommandResponse>>();

        map.TryAdd(shortcuts.FS1, controller.FS1);
        map.TryAdd(shortcuts.FS2, controller.FS2);
        map.TryAdd(shortcuts.FS3, controller.FS3);
        map.TryAdd(shortcuts.FS4, controller.FS4);
        map.TryAdd(shortcuts.FS5, controller.FS5);
        map.TryAdd(shortcuts.FS6, controller.FS6);
        map.TryAdd(shortcuts.FS7, controller.FS7);
        map.TryAdd(shortcuts.FS8, controller.FS8);
        map.TryAdd(shortcuts.Snapshot1, controller.Snapshot1);
        map.TryAdd(shortcuts.Snapshot2, controller.Snapshot2);
        map.TryAdd(shortcuts.Snapshot3, controller.Snapshot3);
        map.TryAdd(shortcuts.Snapshot4, controller.Snapshot4);
        map.TryAdd(shortcuts.NextSnapshot, controller.NextSnapshot);
        map.TryAdd(shortcuts.PreviousSnapshot, controller.PreviousSnapshot);
        map.TryAdd(shortcuts.NextPreset, controller.NextPreset);
        map.TryAdd(shortcuts.PreviousPreset, controller.PreviousPreset);
        map.TryAdd(shortcuts.ToggleTuner, controller.ToggleTuner);

        return map;
    }
}
