#region Milestone 1 - InfiniteMapScroller (Stage 1: bidirectional generation)
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfiniteMapScroller : MonoBehaviour
{
    #region Configuration
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform content;
    [SerializeField] PuzzleLoader puzzleLoader;
    [SerializeField] List<MapTheme> themes;

    [SerializeField] int levelsPerSegment = 25;
    [SerializeField] float segmentHeight = 7500f;
    [SerializeField] float generateThreshold = 0.2f;
    #endregion

    #region State
    Dictionary<int, GameObject> activeSegments = new Dictionary<int, GameObject>();
    int lowestLoadedIndex;
    int highestLoadedIndex;
    int totalLevels;
    #endregion

    #region Unity Lifecycle
    void Start()
    {
        totalLevels = puzzleLoader.puzzles.Length;

        GenerateSegment(0);
        lowestLoadedIndex = 0;
        highestLoadedIndex = 0;

        UpdateContentHeight();
        scrollRect.verticalNormalizedPosition = 0f;

        scrollRect.onValueChanged.AddListener(OnScrolled);
    }
    #endregion

    #region Scroll Handling
    void OnScrolled(Vector2 position)
    {
        if (position.y > 1f - generateThreshold)
        {
            TryGenerateAbove();
        }

        if (position.y < generateThreshold)
        {
            TryGenerateBelow();
        }
    }

    void TryGenerateAbove()
    {
        int nextIndex = highestLoadedIndex + 1;
        int startLevel = nextIndex * levelsPerSegment + 1;
        if (startLevel > totalLevels) return;
        if (activeSegments.ContainsKey(nextIndex)) return;

        GenerateSegment(nextIndex);
        highestLoadedIndex = nextIndex;
        UpdateContentHeight();
    }

    void TryGenerateBelow()
    {
        int prevIndex = lowestLoadedIndex - 1;
        if (prevIndex < 0) return;
        if (activeSegments.ContainsKey(prevIndex)) return;

        GenerateSegment(prevIndex);
        lowestLoadedIndex = prevIndex;
    }
    #endregion

    #region Segment Generation
    void GenerateSegment(int segmentIndex)
    {
        int startLevel = segmentIndex * levelsPerSegment + 1;
        int endLevel = Mathf.Min(startLevel + levelsPerSegment - 1, totalLevels);

        MapTheme theme = GetThemeForLevel(startLevel);
        if (theme == null)
        {
            Debug.LogWarning($"No theme found for level {startLevel}");
            return;
        }

        GameObject segmentObj = Instantiate(theme.segmentPrefab.gameObject, content);
        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
        segmentRect.anchoredPosition = new Vector2(0, segmentIndex * segmentHeight);

        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
        SetupNodesForSegment(segmentView, startLevel, endLevel);

        activeSegments[segmentIndex] = segmentObj;
    }

    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
    {
        string category = puzzleLoader.Category;

        for (int level = startLevel; level <= endLevel; level++)
        {
            int nodeIndex = level - startLevel;
            if (nodeIndex >= segmentView.NodeCount) break;

            PuzzleData puzzle = puzzleLoader.puzzles[level - 1];
            LevelNode node = segmentView.GetNode(nodeIndex);

            bool locked = ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

            node.Setup(puzzle.levelNumber, locked, completed, stars);
        }
    }

    MapTheme GetThemeForLevel(int level)
    {
        foreach (MapTheme theme in themes)
        {
            if (level >= theme.startLevel && level <= theme.endLevel)
            {
                return theme;
            }
        }
        return null;
    }
    #endregion

    #region Content Sizing
    void UpdateContentHeight()
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, (highestLoadedIndex + 1) * segmentHeight);
    }
    #endregion
}
#endregion