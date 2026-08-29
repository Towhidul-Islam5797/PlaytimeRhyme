#region Milestone 1 - MapTheme (level range to segment prefab mapping)
using UnityEngine;

[System.Serializable]
public class MapTheme
{
    public string themeName;
    public int startLevel;
    public int endLevel;
    public MapSegmentView segmentPrefab;
}
#endregion