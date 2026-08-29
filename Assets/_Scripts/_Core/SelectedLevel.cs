// ============================================================
// SETUP INSTRUCTIONS
// ============================================================
// File: Assets/_Scripts/UI/Progress/SelectedLevel.cs
// Action: CREATE this as a new file.
//
// WHAT THIS DOES:
// A tiny static class that holds "which category and level did
// the player just tap on the map" so that value survives the
// scene change from MapScene to GameScene. Static fields keep
// their value across scene loads automatically within one play
// session - no GameObject or DontDestroyOnLoad needed.
//
// NO UNITY INSPECTOR SETUP NEEDED - nothing to attach, nothing
// to wire. Other scripts just read/write SelectedLevel.categoryIndex
// and SelectedLevel.levelNumber directly.
// ============================================================

#region Milestone 1 - SelectedLevel (map-to-gameplay handoff)
public static class SelectedLevel
{
    public static int categoryIndex;
    public static int levelNumber;
}
#endregion