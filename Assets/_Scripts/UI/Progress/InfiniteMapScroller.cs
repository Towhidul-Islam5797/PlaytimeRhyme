#region Milestone 1 - InfiniteMapScroller (Stage 1: bidirectional generation)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InfiniteMapScroller : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] ScrollRect scrollRect;
//    [SerializeField] RectTransform content;
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] List<MapTheme> themes;

//    [SerializeField] int levelsPerSegment = 25;
//    [SerializeField] float segmentHeight = 7500f;
//    [SerializeField] float generateThreshold = 0.2f;
//    #endregion

//    #region State
//    Dictionary<int, GameObject> activeSegments = new Dictionary<int, GameObject>();
//    int lowestLoadedIndex;
//    int highestLoadedIndex;
//    int totalLevels;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        totalLevels = puzzleLoader.puzzles.Length;

//        GenerateSegment(0);
//        lowestLoadedIndex = 0;
//        highestLoadedIndex = 0;

//        UpdateContentHeight();
//        scrollRect.verticalNormalizedPosition = 0f;

//        scrollRect.onValueChanged.AddListener(OnScrolled);
//    }
//    #endregion

//    #region Scroll Handling
//    void OnScrolled(Vector2 position)
//    {
//        if (position.y > 1f - generateThreshold)
//        {
//            TryGenerateAbove();
//        }

//        if (position.y < generateThreshold)
//        {
//            TryGenerateBelow();
//        }
//    }

//    void TryGenerateAbove()
//    {
//        int nextIndex = highestLoadedIndex + 1;
//        int startLevel = nextIndex * levelsPerSegment + 1;
//        if (startLevel > totalLevels) return;
//        if (activeSegments.ContainsKey(nextIndex)) return;

//        GenerateSegment(nextIndex);
//        highestLoadedIndex = nextIndex;
//        UpdateContentHeight();
//    }

//    void TryGenerateBelow()
//    {
//        int prevIndex = lowestLoadedIndex - 1;
//        if (prevIndex < 0) return;
//        if (activeSegments.ContainsKey(prevIndex)) return;

//        GenerateSegment(prevIndex);
//        lowestLoadedIndex = prevIndex;
//    }
//    #endregion

//    #region Segment Generation
//    void GenerateSegment(int segmentIndex)
//    {
//        int startLevel = segmentIndex * levelsPerSegment + 1;
//        if (startLevel > totalLevels) return;

//        MapTheme theme = GetThemeForLevel(startLevel);
//        if (theme == null)
//        {
//            Debug.LogWarning($"No theme found for level {startLevel}");
//            return;
//        }

//        bool isFinalSegment = startLevel + levelsPerSegment - 1 >= totalLevels;
//        MapSegmentView chosenPrefab = isFinalSegment && theme.finalSegmentPrefab != null
//            ? theme.finalSegmentPrefab
//            : theme.segmentVariants[segmentIndex % theme.segmentVariants.Length];

//        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevels);

//        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
//        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
//        float segmentActualHeight = segmentRect.rect.height;
//        segmentRect.anchoredPosition = new Vector2(0, segmentIndex * segmentHeight);

//        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
//        SetupNodesForSegment(segmentView, startLevel, endLevel);

//        activeSegments[segmentIndex] = segmentObj;
//    }

//    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
//    {
//        string category = puzzleLoader.Category;

//        for (int level = startLevel; level <= endLevel; level++)
//        {
//            int nodeIndex = level - startLevel;
//            if (nodeIndex >= segmentView.NodeCount) break;

//            PuzzleData puzzle = puzzleLoader.puzzles[level - 1];
//            LevelNode node = segmentView.GetNode(nodeIndex);

//            bool locked = ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
//            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

//            node.Setup(puzzle.levelNumber, locked, completed, stars);
//        }
//    }

//    MapTheme GetThemeForLevel(int level)
//    {
//        foreach (MapTheme theme in themes)
//        {
//            if (level >= theme.startLevel && level <= theme.endLevel)
//            {
//                return theme;
//            }
//        }
//        return null;
//    }
//    #endregion

//    #region Content Sizing
//    void UpdateContentHeight()
//    {
//        content.sizeDelta = new Vector2(content.sizeDelta.x, (highestLoadedIndex + 1) * segmentHeight);
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - InfiniteMapScroller (variable-size segment generation)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InfiniteMapScroller : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] ScrollRect scrollRect;
//    [SerializeField] RectTransform content;
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] List<MapTheme> themes;
//    [SerializeField] float generateThreshold = 0.2f;
//    #endregion

//    #region Segment Tracking
//    class SegmentInfo
//    {
//        public int startLevel;
//        public int endLevel;
//        public float yPosition;
//        public float height;
//    }

//    List<SegmentInfo> activeSegments = new List<SegmentInfo>();
//    int segmentsGenerated;
//    int totalLevels;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        totalLevels = puzzleLoader.puzzles.Length;

//        GenerateNextSegmentAbove();

//        scrollRect.verticalNormalizedPosition = 0f;
//        scrollRect.onValueChanged.AddListener(OnScrolled);
//    }
//    #endregion

//    #region Scroll Handling
//    void OnScrolled(Vector2 position)
//    {
//        if (position.y > 1f - generateThreshold)
//        {
//            GenerateNextSegmentAbove();
//        }
//    }
//    #endregion

//    #region Segment Generation
//    void GenerateNextSegmentAbove()
//    {
//        int startLevel = activeSegments.Count == 0
//            ? 1
//            : activeSegments[activeSegments.Count - 1].endLevel + 1;

//        if (startLevel > totalLevels) return;

//        MapTheme theme = GetThemeForLevel(startLevel);
//        if (theme == null)
//        {
//            Debug.LogWarning($"No theme found for level {startLevel}");
//            return;
//        }

//        int remaining = totalLevels - startLevel + 1;
//        MapSegmentView tentativeVariant = theme.segmentVariants[segmentsGenerated % theme.segmentVariants.Length];

//        bool useFinal = theme.finalSegmentPrefab != null && remaining <= tentativeVariant.NodeCount;
//        MapSegmentView chosenPrefab = useFinal ? theme.finalSegmentPrefab : tentativeVariant;

//        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevels);

//        float yPosition = activeSegments.Count == 0
//            ? 0f
//            : activeSegments[activeSegments.Count - 1].yPosition + activeSegments[activeSegments.Count - 1].height;

//        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
//        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
//        segmentRect.anchoredPosition = new Vector2(0, yPosition);
//        float height = segmentRect.rect.height;

//        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
//        SetupNodesForSegment(segmentView, startLevel, endLevel);

//        activeSegments.Add(new SegmentInfo
//        {
//            startLevel = startLevel,
//            endLevel = endLevel,
//            yPosition = yPosition,
//            height = height
//        });

//        if (!useFinal) segmentsGenerated++;

//        UpdateContentHeight();
//    }

//    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
//    {
//        string category = puzzleLoader.Category;
//        int nodesUsed = endLevel - startLevel + 1;

//        for (int i = 0; i < segmentView.NodeCount; i++)
//        {
//            LevelNode node = segmentView.GetNode(i);

//            if (i >= nodesUsed)
//            {
//                node.gameObject.SetActive(false);
//                continue;
//            }

//            PuzzleData puzzle = puzzleLoader.puzzles[startLevel - 1 + i];
//            bool locked = ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
//            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

//            node.Setup(puzzle.levelNumber, locked, completed, stars);
//        }
//    }

//    MapTheme GetThemeForLevel(int level)
//    {
//        foreach (MapTheme theme in themes)
//        {
//            if (level >= theme.startLevel && level <= theme.endLevel)
//            {
//                return theme;
//            }
//        }
//        return null;
//    }
//    #endregion

//    #region Content Sizing
//    void UpdateContentHeight()
//    {
//        SegmentInfo last = activeSegments[activeSegments.Count - 1];
//        content.sizeDelta = new Vector2(content.sizeDelta.x, last.yPosition + last.height);
//    }
//    #endregion
//}
#endregion



#region Milestone 1 - InfiniteMapScroller (multi-category, variable-size segment generation)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InfiniteMapScroller : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] ScrollRect scrollRect;
//    [SerializeField] RectTransform content;
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] List<CategoryConfig> categories;
//    [SerializeField] float generateThreshold = 0.2f;
//    #endregion

//    #region Segment Tracking
//    class SegmentInfo
//    {
//        public GameObject segmentObject;
//        public int startLevel;
//        public int endLevel;
//        public float yPosition;
//        public float height;
//    }

//    List<SegmentInfo> activeSegments = new List<SegmentInfo>();
//    int segmentsGenerated;
//    int totalLevels;
//    CategoryConfig activeConfig;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        LoadCategory(0);
//        scrollRect.onValueChanged.AddListener(OnScrolled);
//    }
//    #endregion

//    #region Category Loading
//    public void LoadCategory(int categoryIndex)
//    {
//        foreach (SegmentInfo segment in activeSegments)
//        {
//            Destroy(segment.segmentObject);
//        }
//        activeSegments.Clear();
//        segmentsGenerated = 0;

//        activeConfig = categories[categoryIndex];
//        puzzleLoader.LoadCategory(activeConfig.categoryName, activeConfig.folderPath, activeConfig.csvPath);
//        totalLevels = puzzleLoader.puzzles.Length;

//        GenerateNextSegmentAbove();
//        scrollRect.verticalNormalizedPosition = 0f;
//    }
//    #endregion

//    #region Scroll Handling
//    void OnScrolled(Vector2 position)
//    {
//        if (position.y > 1f - generateThreshold)
//        {
//            GenerateNextSegmentAbove();
//        }
//    }
//    #endregion

//    #region Segment Generation
//    void GenerateNextSegmentAbove()
//    {
//        int startLevel = activeSegments.Count == 0
//            ? 1
//            : activeSegments[activeSegments.Count - 1].endLevel + 1;

//        if (startLevel > totalLevels) return;

//        int remaining = totalLevels - startLevel + 1;
//        MapSegmentView tentativeVariant = activeConfig.segmentVariants[segmentsGenerated % activeConfig.segmentVariants.Length];

//        bool useFinal = activeConfig.finalSegmentPrefab != null && remaining <= tentativeVariant.NodeCount;
//        MapSegmentView chosenPrefab = useFinal ? activeConfig.finalSegmentPrefab : tentativeVariant;

//        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevels);

//        float yPosition = activeSegments.Count == 0
//            ? 0f
//            : activeSegments[activeSegments.Count - 1].yPosition + activeSegments[activeSegments.Count - 1].height;

//        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
//        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
//        segmentRect.anchoredPosition = new Vector2(0, yPosition);
//        float height = segmentRect.rect.height;

//        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
//        SetupNodesForSegment(segmentView, startLevel, endLevel);

//        activeSegments.Add(new SegmentInfo
//        {
//            segmentObject = segmentObj,
//            startLevel = startLevel,
//            endLevel = endLevel,
//            yPosition = yPosition,
//            height = height
//        });

//        if (!useFinal) segmentsGenerated++;

//        UpdateContentHeight();
//    }

//    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
//    {
//        string category = puzzleLoader.Category;
//        int nodesUsed = endLevel - startLevel + 1;

//        for (int i = 0; i < segmentView.NodeCount; i++)
//        {
//            LevelNode node = segmentView.GetNode(i);

//            if (i >= nodesUsed)
//            {
//                node.gameObject.SetActive(false);
//                continue;
//            }

//            PuzzleData puzzle = puzzleLoader.puzzles[startLevel - 1 + i];
//            bool locked = ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
//            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

//            node.Setup(puzzle.levelNumber, locked, completed, stars);
//        }
//    }
//    #endregion

//    #region Content Sizing
//    void UpdateContentHeight()
//    {
//        SegmentInfo last = activeSegments[activeSegments.Count - 1];
//        content.sizeDelta = new Vector2(content.sizeDelta.x, last.yPosition + last.height);
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - InfiniteMapScroller (multi-category, variable-size segment generation)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InfiniteMapScroller : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] ScrollRect scrollRect;
//    [SerializeField] RectTransform content;
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] List<MapTheme> themes;
//    [SerializeField] float generateThreshold = 0.2f;
//    #endregion

//    #region Segment Tracking
//    class SegmentInfo
//    {
//        public GameObject segmentObject;
//        public int startLevel;
//        public int endLevel;
//        public float yPosition;
//        public float height;
//    }

//    List<SegmentInfo> activeSegments = new List<SegmentInfo>();
//    int segmentsGenerated;
//    int totalLevels;
//    MapTheme activeTheme;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        LoadCategory(0);
//        scrollRect.onValueChanged.AddListener(OnScrolled);
//    }
//    #endregion

//    #region Category Loading
//    public void LoadCategory(int categoryIndex)
//    {
//        foreach (SegmentInfo segment in activeSegments)
//        {
//            Destroy(segment.segmentObject);
//        }
//        activeSegments.Clear();
//        segmentsGenerated = 0;

//        puzzleLoader.LoadCategory(categoryIndex);
//        activeTheme = themes[categoryIndex];
//        totalLevels = puzzleLoader.puzzles.Length;

//        GenerateNextSegmentAbove();
//        scrollRect.verticalNormalizedPosition = 0f;
//    }
//    #endregion

//    #region Scroll Handling
//    void OnScrolled(Vector2 position)
//    {
//        if (position.y > 1f - generateThreshold)
//        {
//            GenerateNextSegmentAbove();
//        }
//    }
//    #endregion

//    #region Segment Generation
//    void GenerateNextSegmentAbove()
//    {
//        int startLevel = activeSegments.Count == 0
//            ? 1
//            : activeSegments[activeSegments.Count - 1].endLevel + 1;

//        if (startLevel > totalLevels) return;

//        int remaining = totalLevels - startLevel + 1;
//        MapSegmentView tentativeVariant = activeTheme.segmentVariants[segmentsGenerated % activeTheme.segmentVariants.Length];

//        bool useFinal = activeTheme.finalSegmentPrefab != null && remaining <= tentativeVariant.NodeCount;
//        MapSegmentView chosenPrefab = useFinal ? activeTheme.finalSegmentPrefab : tentativeVariant;

//        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevels);

//        float yPosition = activeSegments.Count == 0
//            ? 0f
//            : activeSegments[activeSegments.Count - 1].yPosition + activeSegments[activeSegments.Count - 1].height;

//        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
//        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
//        segmentRect.anchoredPosition = new Vector2(0, yPosition);
//        float height = segmentRect.rect.height;

//        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
//        SetupNodesForSegment(segmentView, startLevel, endLevel);

//        activeSegments.Add(new SegmentInfo
//        {
//            segmentObject = segmentObj,
//            startLevel = startLevel,
//            endLevel = endLevel,
//            yPosition = yPosition,
//            height = height
//        });

//        if (!useFinal) segmentsGenerated++;

//        UpdateContentHeight();
//    }

//    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
//    {
//        string category = puzzleLoader.Category;
//        int nodesUsed = endLevel - startLevel + 1;

//        for (int i = 0; i < segmentView.NodeCount; i++)
//        {
//            LevelNode node = segmentView.GetNode(i);

//            if (i >= nodesUsed)
//            {
//                node.gameObject.SetActive(false);
//                continue;
//            }

//            PuzzleData puzzle = puzzleLoader.puzzles[startLevel - 1 + i];
//            bool locked = ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
//            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

//            node.Setup(puzzle.levelNumber, locked, completed, stars);
//        }
//    }
//    #endregion

//    #region Content Sizing
//    void UpdateContentHeight()
//    {
//        SegmentInfo last = activeSegments[activeSegments.Count - 1];
//        content.sizeDelta = new Vector2(content.sizeDelta.x, last.yPosition + last.height);
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - InfiniteMapScroller (multi-category, auto-continuing segment generation)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InfiniteMapScroller : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] ScrollRect scrollRect;
//    [SerializeField] RectTransform content;
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] List<MapTheme> themes;
//    [SerializeField] float generateThreshold = 0.2f;
//    #endregion

//    #region Segment Tracking
//    class SegmentInfo
//    {
//        public GameObject segmentObject;
//        public int categoryIndex;
//        public int startLevel;
//        public int endLevel;
//        public float yPosition;
//        public float height;
//    }

//    List<SegmentInfo> activeSegments = new List<SegmentInfo>();
//    int segmentsGeneratedInCategory;
//    int currentCategoryIndex;
//    int totalLevelsInCategory;
//    MapTheme activeTheme;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        StartAtCategory(0);
//        scrollRect.onValueChanged.AddListener(OnScrolled);
//    }
//    #endregion

//    #region Category Loading
//    public void StartAtCategory(int categoryIndex)
//    {
//        foreach (SegmentInfo segment in activeSegments)
//        {
//            Destroy(segment.segmentObject);
//        }
//        activeSegments.Clear();

//        SwitchToCategory(categoryIndex);

//        GenerateNextSegmentAbove();
//        scrollRect.verticalNormalizedPosition = 0f;
//    }

//    void SwitchToCategory(int categoryIndex)
//    {
//        currentCategoryIndex = categoryIndex;
//        segmentsGeneratedInCategory = 0;

//        puzzleLoader.LoadCategory(categoryIndex);
//        activeTheme = themes[categoryIndex];
//        totalLevelsInCategory = puzzleLoader.puzzles.Length;
//    }
//    #endregion

//    #region Scroll Handling
//    void OnScrolled(Vector2 position)
//    {
//        if (position.y > 1f - generateThreshold)
//        {
//            GenerateNextSegmentAbove();
//        }
//    }
//    #endregion

//    #region Segment Generation
//    void GenerateNextSegmentAbove()
//    {
//        SegmentInfo lastSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
//        int startLevel = lastSegment == null ? 1 : lastSegment.endLevel + 1;

//        if (startLevel > totalLevelsInCategory)
//        {
//            int nextCategoryIndex = currentCategoryIndex + 1;
//            if (nextCategoryIndex >= themes.Count) return;

//            SwitchToCategory(nextCategoryIndex);
//            startLevel = 1;
//        }

//        int remaining = totalLevelsInCategory - startLevel + 1;
//        MapSegmentView tentativeVariant = activeTheme.segmentVariants[segmentsGeneratedInCategory % activeTheme.segmentVariants.Length];

//        bool useFinal = activeTheme.finalSegmentPrefab != null && remaining <= tentativeVariant.NodeCount;
//        MapSegmentView chosenPrefab = useFinal ? activeTheme.finalSegmentPrefab : tentativeVariant;

//        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevelsInCategory);

//        SegmentInfo previousSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
//        float yPosition = previousSegment == null ? 0f : previousSegment.yPosition + previousSegment.height;

//        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
//        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
//        segmentRect.anchoredPosition = new Vector2(0, yPosition);
//        float height = segmentRect.rect.height;

//        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
//        SetupNodesForSegment(segmentView, startLevel, endLevel);

//        activeSegments.Add(new SegmentInfo
//        {
//            segmentObject = segmentObj,
//            categoryIndex = currentCategoryIndex,
//            startLevel = startLevel,
//            endLevel = endLevel,
//            yPosition = yPosition,
//            height = height
//        });

//        if (!useFinal) segmentsGeneratedInCategory++;

//        UpdateContentHeight();
//    }

//    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
//    {
//        string category = puzzleLoader.Category;
//        int nodesUsed = endLevel - startLevel + 1;

//        for (int i = 0; i < segmentView.NodeCount; i++)
//        {
//            LevelNode node = segmentView.GetNode(i);

//            if (i >= nodesUsed)
//            {
//                node.gameObject.SetActive(false);
//                continue;
//            }

//            PuzzleData puzzle = puzzleLoader.puzzles[startLevel - 1 + i];
//            bool locked = ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
//            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

//            node.Setup(puzzle.levelNumber, locked, completed, stars);
//        }
//    }
//    #endregion

//    #region Content Sizing
//    void UpdateContentHeight()
//    {
//        SegmentInfo last = activeSegments[activeSegments.Count - 1];
//        content.sizeDelta = new Vector2(content.sizeDelta.x, last.yPosition + last.height);
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - InfiniteMapScroller (multi-category, auto-continuing segment generation)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InfiniteMapScroller : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] ScrollRect scrollRect;
//    [SerializeField] RectTransform content;
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] List<MapTheme> themes;
//    [SerializeField] float generateThreshold = 0.2f;
//    #endregion

//    #region Segment Tracking
//    class SegmentInfo
//    {
//        public GameObject segmentObject;
//        public int categoryIndex;
//        public int startLevel;
//        public int endLevel;
//        public float yPosition;
//        public float height;
//    }

//    List<SegmentInfo> activeSegments = new List<SegmentInfo>();
//    int segmentsGeneratedInCategory;
//    int currentCategoryIndex;
//    int totalLevelsInCategory;
//    MapTheme activeTheme;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        StartAtCategory(0);
//        scrollRect.onValueChanged.AddListener(OnScrolled);
//    }
//    #endregion

//    #region Category Loading
//    public void StartAtCategory(int categoryIndex)
//    {
//        foreach (SegmentInfo segment in activeSegments)
//        {
//            Destroy(segment.segmentObject);
//        }
//        activeSegments.Clear();

//        SwitchToCategory(categoryIndex);

//        GenerateNextSegmentAbove();
//        scrollRect.verticalNormalizedPosition = 0f;
//    }

//    void SwitchToCategory(int categoryIndex)
//    {
//        currentCategoryIndex = categoryIndex;
//        segmentsGeneratedInCategory = 0;

//        puzzleLoader.LoadCategory(categoryIndex);
//        activeTheme = themes[categoryIndex];
//        totalLevelsInCategory = puzzleLoader.puzzles.Length;
//    }
//    #endregion

//    #region Scroll Handling
//    void OnScrolled(Vector2 position)
//    {
//        if (position.y > 1f - generateThreshold)
//        {
//            GenerateNextSegmentAbove();
//        }
//    }
//    #endregion

//    #region Segment Generation
//    void GenerateNextSegmentAbove()
//    {
//        SegmentInfo lastSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
//        int startLevel = lastSegment == null ? 1 : lastSegment.endLevel + 1;

//        if (startLevel > totalLevelsInCategory)
//        {
//            int nextCategoryIndex = currentCategoryIndex + 1;
//            if (nextCategoryIndex >= themes.Count) return;

//            SwitchToCategory(nextCategoryIndex);
//            startLevel = 1;
//        }

//        if (activeTheme.segmentVariants == null || activeTheme.segmentVariants.Length == 0)
//        {
//            Debug.LogWarning($"Category '{activeTheme.themeName}' has no segment variants assigned yet - stopping generation here.");
//            return;
//        }

//        int remaining = totalLevelsInCategory - startLevel + 1;
//        MapSegmentView tentativeVariant = activeTheme.segmentVariants[segmentsGeneratedInCategory % activeTheme.segmentVariants.Length];

//        bool useFinal = activeTheme.finalSegmentPrefab != null && remaining <= tentativeVariant.NodeCount;
//        MapSegmentView chosenPrefab = useFinal ? activeTheme.finalSegmentPrefab : tentativeVariant;

//        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevelsInCategory);

//        SegmentInfo previousSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
//        float yPosition = previousSegment == null ? 0f : previousSegment.yPosition + previousSegment.height;

//        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
//        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
//        segmentRect.anchoredPosition = new Vector2(0, yPosition);
//        float height = segmentRect.rect.height;

//        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
//        SetupNodesForSegment(segmentView, startLevel, endLevel);

//        activeSegments.Add(new SegmentInfo
//        {
//            segmentObject = segmentObj,
//            categoryIndex = currentCategoryIndex,
//            startLevel = startLevel,
//            endLevel = endLevel,
//            yPosition = yPosition,
//            height = height
//        });

//        if (!useFinal) segmentsGeneratedInCategory++;

//        UpdateContentHeight();
//    }

//    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
//    {
//        string category = puzzleLoader.Category;
//        int nodesUsed = endLevel - startLevel + 1;

//        for (int i = 0; i < segmentView.NodeCount; i++)
//        {
//            LevelNode node = segmentView.GetNode(i);

//            if (i >= nodesUsed)
//            {
//                node.gameObject.SetActive(false);
//                continue;
//            }

//            PuzzleData puzzle = puzzleLoader.puzzles[startLevel - 1 + i];
//            bool locked = ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
//            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

//            node.Setup(puzzle.levelNumber, locked, completed, stars);
//        }
//    }
//    #endregion

//    #region Content Sizing
//    void UpdateContentHeight()
//    {
//        SegmentInfo last = activeSegments[activeSegments.Count - 1];
//        content.sizeDelta = new Vector2(content.sizeDelta.x, last.yPosition + last.height);
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - InfiniteMapScroller (multi-category, category-locked segment generation)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class InfiniteMapScroller : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] ScrollRect scrollRect;
//    [SerializeField] RectTransform content;
//    [SerializeField] PuzzleLoader puzzleLoader;
//    [SerializeField] List<MapTheme> themes;
//    [SerializeField] float generateThreshold = 0.2f;
//    #endregion

//    #region Segment Tracking
//    class SegmentInfo
//    {
//        public GameObject segmentObject;
//        public int categoryIndex;
//        public int startLevel;
//        public int endLevel;
//        public float yPosition;
//        public float height;
//    }

//    List<SegmentInfo> activeSegments = new List<SegmentInfo>();
//    int segmentsGeneratedInCategory;
//    int currentCategoryIndex;
//    int totalLevelsInCategory;
//    bool categoryUnlocked;
//    MapTheme activeTheme;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        StartAtCategory(0);
//        scrollRect.onValueChanged.AddListener(OnScrolled);
//    }
//    #endregion

//    #region Category Loading
//    public void StartAtCategory(int categoryIndex)
//    {
//        foreach (SegmentInfo segment in activeSegments)
//        {
//            Destroy(segment.segmentObject);
//        }
//        activeSegments.Clear();

//        SwitchToCategory(categoryIndex);

//        GenerateNextSegmentAbove();
//        scrollRect.verticalNormalizedPosition = 0f;
//    }

//    void SwitchToCategory(int categoryIndex)
//    {
//        currentCategoryIndex = categoryIndex;
//        segmentsGeneratedInCategory = 0;

//        puzzleLoader.LoadCategory(categoryIndex);
//        activeTheme = themes[categoryIndex];
//        totalLevelsInCategory = puzzleLoader.puzzles.Length;
//        categoryUnlocked = ProgressManager.IsCategoryUnlocked(categoryIndex, puzzleLoader);
//    }
//    #endregion

//    #region Scroll Handling
//    void OnScrolled(Vector2 position)
//    {
//        if (position.y > 1f - generateThreshold)
//        {
//            GenerateNextSegmentAbove();
//        }
//    }
//    #endregion

//    #region Segment Generation
//    void GenerateNextSegmentAbove()
//    {
//        SegmentInfo lastSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
//        int startLevel = lastSegment == null ? 1 : lastSegment.endLevel + 1;

//        if (startLevel > totalLevelsInCategory)
//        {
//            int nextCategoryIndex = currentCategoryIndex + 1;
//            if (nextCategoryIndex >= themes.Count) return;

//            SwitchToCategory(nextCategoryIndex);
//            startLevel = 1;
//        }

//        if (activeTheme.segmentVariants == null || activeTheme.segmentVariants.Length == 0)
//        {
//            Debug.LogWarning($"Category '{activeTheme.themeName}' has no segment variants assigned yet - stopping generation here.");
//            return;
//        }

//        int remaining = totalLevelsInCategory - startLevel + 1;
//        MapSegmentView tentativeVariant = activeTheme.segmentVariants[segmentsGeneratedInCategory % activeTheme.segmentVariants.Length];

//        bool useFinal = activeTheme.finalSegmentPrefab != null && remaining <= tentativeVariant.NodeCount;
//        MapSegmentView chosenPrefab = useFinal ? activeTheme.finalSegmentPrefab : tentativeVariant;

//        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevelsInCategory);

//        SegmentInfo previousSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
//        float yPosition = previousSegment == null ? 0f : previousSegment.yPosition + previousSegment.height;

//        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
//        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
//        segmentRect.anchoredPosition = new Vector2(0, yPosition);
//        float height = segmentRect.rect.height;

//        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
//        SetupNodesForSegment(segmentView, startLevel, endLevel);

//        activeSegments.Add(new SegmentInfo
//        {
//            segmentObject = segmentObj,
//            categoryIndex = currentCategoryIndex,
//            startLevel = startLevel,
//            endLevel = endLevel,
//            yPosition = yPosition,
//            height = height
//        });

//        if (!useFinal) segmentsGeneratedInCategory++;

//        UpdateContentHeight();
//    }

//    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
//    {
//        string category = puzzleLoader.Category;
//        int nodesUsed = endLevel - startLevel + 1;

//        for (int i = 0; i < segmentView.NodeCount; i++)
//        {
//            LevelNode node = segmentView.GetNode(i);

//            if (i >= nodesUsed)
//            {
//                node.gameObject.SetActive(false);
//                continue;
//            }

//            PuzzleData puzzle = puzzleLoader.puzzles[startLevel - 1 + i];

//            bool locked = !categoryUnlocked || ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
//            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
//            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

//            node.Setup(currentCategoryIndex, puzzle.levelNumber, locked, completed, stars);
//        }
//    }
//    #endregion

//    #region Content Sizing
//    void UpdateContentHeight()
//    {
//        SegmentInfo last = activeSegments[activeSegments.Count - 1];
//        content.sizeDelta = new Vector2(content.sizeDelta.x, last.yPosition + last.height);
//    }
//    #endregion
//}
#endregion

// ============================================================
// SETUP INSTRUCTIONS
// ============================================================
// File: Assets/_Scripts/UI/Progress/InfiniteMapScroller.cs
// Action: REPLACE the entire file with this content.
//
// WHAT CHANGED FROM YOUR CURRENT VERSION:
// - Start() no longer always begins at category 0. It now calls
//   FindResumePoint() to figure out where the player's progress
//   actually is (the first level they haven't completed yet),
//   then jumps straight to that category and generates segments
//   up through the one containing that level - then scrolls the
//   view to land on it.
// - GenerateNextSegmentAbove() was split: the actual "build one
//   segment" logic moved into a new method, GenerateOneSegment(),
//   which returns the SegmentInfo it created (or null if nothing
//   could be generated). GenerateNextSegmentAbove() now just
//   calls that and ignores the return value - used for normal
//   scroll-triggered generation, unchanged in behavior.
// - New method: ScrollToSegment() - moves the ScrollRect to show
//   a specific segment. This uses an approximate calculation
//   (segment position divided by total scrollable range), not a
//   pixel-perfect one - good enough to land in the right area,
//   not guaranteed to be exact to the pixel.
//
// NO NEW INSPECTOR FIELDS - nothing new to wire up. Same Scroll
// Rect, Content, Puzzle Loader, Themes as before.
//
// ONE THING TO KNOW: categories before the resume point are NOT
// generated at all (since they're already fully completed, there
// is no need to build their segments just to scroll past them).
// This means a fresh map load will not let you scroll backward
// into an already-finished category - only forward from your
// current progress. If you want to browse a finished category
// again, that still needs the "jump to category" selection screen
// (Option B from our earlier discussion), which is separate,
// still-unbuilt work.
// ============================================================

#region Milestone 1 - InfiniteMapScroller (resumes at current progress on load)
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
    [SerializeField] float generateThreshold = 0.2f;
    #endregion

    #region Segment Tracking
    class SegmentInfo
    {
        public GameObject segmentObject;
        public int categoryIndex;
        public int startLevel;
        public int endLevel;
        public float yPosition;
        public float height;
    }

    List<SegmentInfo> activeSegments = new List<SegmentInfo>();
    int segmentsGeneratedInCategory;
    int currentCategoryIndex;
    int totalLevelsInCategory;
    bool categoryUnlocked;
    MapTheme activeTheme;
    #endregion

    #region Unity Lifecycle
    void Start()
    {
        (int resumeCategoryIndex, int resumeLevelNumber) = FindResumePoint();
        ResumeAtLevel(resumeCategoryIndex, resumeLevelNumber);

        scrollRect.onValueChanged.AddListener(OnScrolled);
    }
    #endregion

    #region Resume Point
    (int categoryIndex, int levelNumber) FindResumePoint()
    {
        for (int i = 0; i < puzzleLoader.CategoryCount; i++)
        {
            PuzzleLoader.CategoryEntry entry = puzzleLoader.GetCategoryEntry(i);

            for (int level = 1; level <= entry.totalLevels; level++)
            {
                if (!ProgressManager.IsLevelCompleted(entry.categoryName, level))
                {
                    return (i, level);
                }
            }
        }

        int lastIndex = puzzleLoader.CategoryCount - 1;
        int lastLevel = puzzleLoader.GetCategoryEntry(lastIndex).totalLevels;
        return (lastIndex, lastLevel);
    }

    void ResumeAtLevel(int categoryIndex, int levelNumber)
    {
        foreach (SegmentInfo segment in activeSegments)
        {
            Destroy(segment.segmentObject);
        }
        activeSegments.Clear();

        SwitchToCategory(categoryIndex);

        SegmentInfo targetSegment = null;
        while (targetSegment == null)
        {
            SegmentInfo generated = GenerateOneSegment();
            if (generated == null) break;

            if (levelNumber >= generated.startLevel && levelNumber <= generated.endLevel)
            {
                targetSegment = generated;
            }
        }

        ScrollToSegment(targetSegment);
    }

    void ScrollToSegment(SegmentInfo segment)
    {
        if (segment == null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
            return;
        }

        float viewportHeight = scrollRect.viewport.rect.height;
        float scrollableRange = Mathf.Max(content.sizeDelta.y - viewportHeight, 0.0001f);
        float normalized = Mathf.Clamp01(segment.yPosition / scrollableRange);

        scrollRect.verticalNormalizedPosition = normalized;
    }
    #endregion

    #region Category Loading
    void SwitchToCategory(int categoryIndex)
    {
        currentCategoryIndex = categoryIndex;
        segmentsGeneratedInCategory = 0;

        puzzleLoader.LoadCategory(categoryIndex);
        activeTheme = themes[categoryIndex];
        totalLevelsInCategory = puzzleLoader.puzzles.Length;
        categoryUnlocked = ProgressManager.IsCategoryUnlocked(categoryIndex, puzzleLoader);
    }
    #endregion

    #region Scroll Handling
    void OnScrolled(Vector2 position)
    {
        if (position.y > 1f - generateThreshold)
        {
            GenerateNextSegmentAbove();
        }
    }
    #endregion

    #region Segment Generation
    void GenerateNextSegmentAbove()
    {
        GenerateOneSegment();
    }

    SegmentInfo GenerateOneSegment()
    {
        SegmentInfo lastSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
        int startLevel = lastSegment == null ? 1 : lastSegment.endLevel + 1;

        if (startLevel > totalLevelsInCategory)
        {
            int nextCategoryIndex = currentCategoryIndex + 1;
            if (nextCategoryIndex >= themes.Count) return null;

            SwitchToCategory(nextCategoryIndex);
            startLevel = 1;
        }

        if (activeTheme.segmentVariants == null || activeTheme.segmentVariants.Length == 0)
        {
            Debug.LogWarning($"Category '{activeTheme.themeName}' has no segment variants assigned yet - stopping generation here.");
            return null;
        }

        int remaining = totalLevelsInCategory - startLevel + 1;
        MapSegmentView tentativeVariant = activeTheme.segmentVariants[segmentsGeneratedInCategory % activeTheme.segmentVariants.Length];

        bool useFinal = activeTheme.finalSegmentPrefab != null && remaining <= tentativeVariant.NodeCount;
        MapSegmentView chosenPrefab = useFinal ? activeTheme.finalSegmentPrefab : tentativeVariant;

        int endLevel = Mathf.Min(startLevel + chosenPrefab.NodeCount - 1, totalLevelsInCategory);

        SegmentInfo previousSegment = activeSegments.Count == 0 ? null : activeSegments[activeSegments.Count - 1];
        float yPosition = previousSegment == null ? 0f : previousSegment.yPosition + previousSegment.height;

        GameObject segmentObj = Instantiate(chosenPrefab.gameObject, content);
        RectTransform segmentRect = segmentObj.GetComponent<RectTransform>();
        segmentRect.anchoredPosition = new Vector2(0, yPosition);
        float height = segmentRect.rect.height;

        MapSegmentView segmentView = segmentObj.GetComponent<MapSegmentView>();
        SetupNodesForSegment(segmentView, startLevel, endLevel);

        SegmentInfo newSegment = new SegmentInfo
        {
            segmentObject = segmentObj,
            categoryIndex = currentCategoryIndex,
            startLevel = startLevel,
            endLevel = endLevel,
            yPosition = yPosition,
            height = height
        };
        activeSegments.Add(newSegment);

        if (!useFinal) segmentsGeneratedInCategory++;

        UpdateContentHeight();

        return newSegment;
    }

    void SetupNodesForSegment(MapSegmentView segmentView, int startLevel, int endLevel)
    {
        string category = puzzleLoader.Category;
        int nodesUsed = endLevel - startLevel + 1;

        for (int i = 0; i < segmentView.NodeCount; i++)
        {
            LevelNode node = segmentView.GetNode(i);

            if (i >= nodesUsed)
            {
                node.gameObject.SetActive(false);
                continue;
            }

            PuzzleData puzzle = puzzleLoader.puzzles[startLevel - 1 + i];

            bool locked = !categoryUnlocked || ProgressManager.IsLevelLocked(category, puzzle.levelNumber);
            bool completed = ProgressManager.IsLevelCompleted(category, puzzle.levelNumber);
            int stars = ProgressManager.GetStars(category, puzzle.levelNumber);

            node.Setup(currentCategoryIndex, puzzle.levelNumber, locked, completed, stars);
        }
    }
    #endregion

    #region Content Sizing
    void UpdateContentHeight()
    {
        SegmentInfo last = activeSegments[activeSegments.Count - 1];
        content.sizeDelta = new Vector2(content.sizeDelta.x, last.yPosition + last.height);
    }
    #endregion
}
#endregion