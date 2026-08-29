#region Milestone 1 - ProgressManager (minimal save system)
//using UnityEngine;

//public static class ProgressManager
//{
//    public static int GetStars(string category, int levelNumber)
//    {
//        string key = GetKey(category, levelNumber);
//        return PlayerPrefs.GetInt(key, 0);
//    }

//    public static void SetStars(string category, int levelNumber, int stars)
//    {
//        int currentBest = GetStars(category, levelNumber);
//        if (stars > currentBest)
//        {
//            string key = GetKey(category, levelNumber);
//            PlayerPrefs.SetInt(key, stars);
//            PlayerPrefs.Save();
//        }
//    }

//    public static bool IsLevelCompleted(string category, int levelNumber)
//    {
//        return GetStars(category, levelNumber) > 0;
//    }

//    public static bool IsLevelLocked(string category, int levelNumber)
//    {
//        if (levelNumber <= 1) return false;
//        return !IsLevelCompleted(category, levelNumber - 1);
//    }

//    static string GetKey(string category, int levelNumber)
//    {
//        return $"{category}_Level_{levelNumber}_Stars";
//    }
//}
#endregion

#region Milestone 1 - ProgressManager (minimal save system, category unlock)
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

    public static bool IsCategoryUnlocked(int categoryIndex, PuzzleLoader puzzleLoader)
    {
        if (categoryIndex <= 0) return true;

        PuzzleLoader.CategoryEntry previousCategory = puzzleLoader.GetCategoryEntry(categoryIndex - 1);
        return IsLevelCompleted(previousCategory.categoryName, previousCategory.totalLevels);
    }

    static string GetKey(string category, int levelNumber)
    {
        return $"{category}_Level_{levelNumber}_Stars";
    }
}
#endregion