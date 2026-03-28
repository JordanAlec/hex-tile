namespace Core.Models.Configuration;

public class KeyboardShortcutsSettingsData
{
    public string FS1 { get; set; } = string.Empty;
    public string FS2 { get; set; } = string.Empty;
    public string FS3 { get; set; } = string.Empty;
    public string FS4 { get; set; } = string.Empty;
    public string FS5 { get; set; } = string.Empty;
    public string FS6 { get; set; } = string.Empty;
    public string FS7 { get; set; } = string.Empty;
    public string FS8 { get; set; } = string.Empty;
    public string Snapshot1 { get; set; } = string.Empty;
    public string Snapshot2 { get; set; } = string.Empty;
    public string Snapshot3 { get; set; } = string.Empty;
    public string Snapshot4 { get; set; } = string.Empty;
    public string NextSnapshot { get; set; } = string.Empty;
    public string PreviousSnapshot { get; set; } = string.Empty;
    public string NextPreset { get; set; } = string.Empty;
    public string PreviousPreset { get; set; } = string.Empty;
    public string ToggleTuner { get; set; } = string.Empty;

    public IEnumerable<string> Shortcuts =>
    [
        FS1, FS2, FS3, FS4, FS5, FS6, FS7, FS8,
        Snapshot1, Snapshot2, Snapshot3, Snapshot4,
        NextSnapshot, PreviousSnapshot, NextPreset, PreviousPreset,
        ToggleTuner
    ];
}
