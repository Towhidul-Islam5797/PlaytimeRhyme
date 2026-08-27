#region Milestone 2 - ProgressManager (minimal save system)
using UnityEngine;

public static class ProgressManager
{
    public static int GetStars(string category, int levelNumber)
    {
        string key = GetKey(category, levelNumber);
        return PlayerPrefs.GetInt(key, 0);
    }

    public static void SetStars(string category, int levelNumber, int stars)
    {
        int currentBest = GetStars(category, levelNumber);
        if (stars > currentBest)
        {
            string key = GetKey(category, levelNumber);
            PlayerPrefs.SetInt(key, stars);
            PlayerPrefs.Save();
        }
    }

    public static bool IsLevelCompleted(string category, int levelNumber)
    {
        return GetStars(category, levelNumber) > 0;
    }

    public static bool IsLevelLocked(string category, int levelNumber)
    {
        if (levelNumber <= 1) return false;
        return !IsLevelCompleted(category, levelNumber - 1);
    }

    static string GetKey(string category, int levelNumber)
    {
        return $"{category}_Level_{levelNumber}_Stars";
    }
}
#endregion