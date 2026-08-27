#region Milestone 2 - LevelPathBuilder (dynamic node spawning)
//using UnityEngine;

//public class LevelPathBuilder : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] LevelNode levelNodePrefab;
//    [SerializeField] RectTransform content;

//    [SerializeField] float verticalSpacing = 250f;
//    [SerializeField] float[] horizontalPattern = { 0f, 200f, 0f, -200f };
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        BuildPath();
//    }
//    #endregion

//    #region Building
//    void BuildPath()
//    {
//        int levelCount = puzzleLoader.puzzles.Length;
//        string category = puzzleLoader.Category;

//        content.sizeDelta = new Vector2(content.sizeDelta.x, levelCount * verticalSpacing);

//        for (int i = 0; i < levelCount; i++)
//        {
//            int levelNumber = puzzleLoader.puzzles[i].levelNumber;

//            LevelNode node = Instantiate(levelNodePrefab, content);
//            RectTransform nodeRect = node.GetComponent<RectTransform>();

//            float xPos = horizontalPattern[i % horizontalPattern.Length];
//            float yPos = -(i * verticalSpacing);
//            nodeRect.anchoredPosition = new Vector2(xPos, yPos);

//            bool locked = ProgressManager.IsLevelLocked(category, levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, levelNumber);
//            int stars = ProgressManager.GetStars(category, levelNumber);

//            node.Setup(levelNumber, locked, completed, stars);
//        }
//    }
//    #endregion
//}
#endregion

#region Milestone 2 - LevelPathBuilder (marker-based node spawning)
using UnityEngine;
using UnityEngine.UI;

public class LevelPathBuilder : MonoBehaviour
{
    #region Configuration
    [SerializeField] PuzzleLoader puzzleLoader;
    [SerializeField] LevelNode levelNodePrefab;
    [SerializeField] RectTransform markersContainer;
    [SerializeField] ScrollRect scrollRect;
    #endregion

    #region Unity Lifecycle
    void Start()
    {
        BuildPath();
        scrollRect.verticalNormalizedPosition = 0f;
    }
    #endregion

    #region Building
    void BuildPath()
    {
        int puzzleCount = puzzleLoader.puzzles.Length;
        int markerCount = markersContainer.childCount;
        string category = puzzleLoader.Category;

        if (markerCount != puzzleCount)
        {
            Debug.LogWarning($"Marker count ({markerCount}) does not match puzzle count ({puzzleCount}). Using the smaller of the two.");
        }

        int nodeCount = Mathf.Min(markerCount, puzzleCount);

        for (int i = 0; i < nodeCount; i++)
        {
            RectTransform marker = markersContainer.GetChild(i) as RectTransform;
            int levelNumber = puzzleLoader.puzzles[i].levelNumber;

            LevelNode node = Instantiate(levelNodePrefab, markersContainer.parent);
            RectTransform nodeRect = node.GetComponent<RectTransform>();
            nodeRect.anchoredPosition = marker.anchoredPosition;

            bool locked = ProgressManager.IsLevelLocked(category, levelNumber);
            bool completed = ProgressManager.IsLevelCompleted(category, levelNumber);
            int stars = ProgressManager.GetStars(category, levelNumber);

            node.Setup(levelNumber, locked, completed, stars);
        }
    }
    #endregion
}
#endregion