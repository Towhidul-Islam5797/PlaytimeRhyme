#region Summary
/// This class is responsible for managing the game session, including tracking the current puzzle index and providing access to the current puzzle data and sprite. 
/// It interacts with the PuzzleLoader to retrieve puzzle information and determine if there are more puzzles to load.
/// Usage: 
/// 1. Attach this script to a GameObject in your Unity scene.
/// 2. Assign a PuzzleLoader instance to the 'loader' field in the inspector. 
/// 3. Use GetCurrentPuzzle() to retrieve the current puzzle data, HasNextPuzzle() to check if there are more puzzles, and LoadNextPuzzle() to advance to the next puzzle.
/// Note: Ensure that the PuzzleLoader is properly set up and contains the necessary puzzle data for this class to function correctly.
#endregion
#region Phase 1 Sprint 5 - GameSession (sequential puzzle tracking)
//using UnityEngine;

//public class GameSession : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] PuzzleLoader loader;
//    #endregion

//    #region State
//    int currentIndex = 0;
//    #endregion

//    #region Public Methods
//    public PuzzleData GetCurrentPuzzle()
//    {
//        return loader.puzzles[currentIndex];
//    }

//    public bool HasNextPuzzle()
//    {
//        return currentIndex + 1 < loader.puzzles.Length;
//    }

//    public void LoadNextPuzzle()
//    {
//        if (HasNextPuzzle())
//        {
//            currentIndex++;
//        }
//    }

//    public Sprite GetCurrentSprite()
//    {
//        return loader.GetSpriteForPuzzle(GetCurrentPuzzle());
//    }
//    #endregion
//}
#endregion

#region Phase 1 Sprint 5 - GameSession (puzzle display and gameplay)
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class GameSession : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] PuzzleLoader loader;
//    [SerializeField] Image puzzleImageDisplay;
//    [SerializeField] Transform tileTray;
//    [SerializeField] Transform answerSlotRow;
//    [SerializeField] GameObject tilePrefab;
//    [SerializeField] GameObject slotPrefab;
//    #endregion

//    #region State
//    int currentIndex = 0;
//    List<AnswerSlot> currentSlots = new List<AnswerSlot>();
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        LoadPuzzle();
//    }
//    #endregion

//    #region Puzzle Navigation
//    public PuzzleData GetCurrentPuzzle()
//    {
//        return loader.puzzles[currentIndex];
//    }

//    public bool HasNextPuzzle()
//    {
//        return currentIndex + 1 < loader.puzzles.Length;
//    }

//    public void LoadNextPuzzle()
//    {
//        if (HasNextPuzzle())
//        {
//            currentIndex++;
//            LoadPuzzle();
//        }
//    }
//    #endregion

//    #region Puzzle Setup
//    void LoadPuzzle()
//    {
//        ClearTray();
//        ClearSlots();

//        PuzzleData puzzle = GetCurrentPuzzle();

//        puzzleImageDisplay.sprite = loader.GetSpriteForPuzzle(puzzle);

//        SpawnSlots(puzzle.answerWords);
//        SpawnTiles(puzzle.jumbleLetters);
//    }

//    void ClearTray()
//    {
//        foreach (Transform child in tileTray)
//        {
//            Destroy(child.gameObject);
//        }
//    }

//    void ClearSlots()
//    {
//        foreach (Transform child in answerSlotRow)
//        {
//            Destroy(child.gameObject);
//        }
//        currentSlots.Clear();
//    }

//    void SpawnSlots(string[] answerWords)
//    {
//        for (int w = 0; w < answerWords.Length; w++)
//        {
//            string word = answerWords[w];

//            for (int i = 0; i < word.Length; i++)
//            {
//                GameObject slotObject = Instantiate(slotPrefab, answerSlotRow);
//                AnswerSlot slot = slotObject.GetComponent<AnswerSlot>();
//                currentSlots.Add(slot);
//            }

//            bool isLastWord = (w == answerWords.Length - 1);
//            if (!isLastWord)
//            {
//                SpawnWordGap();
//            }
//        }
//    }

//    void SpawnWordGap()
//    {
//        GameObject gap = new GameObject("WordGap", typeof(RectTransform));
//        gap.transform.SetParent(answerSlotRow, false);
//        LayoutElement layoutElement = gap.AddComponent<LayoutElement>();
//        layoutElement.minWidth = 40f;
//    }

//    void SpawnTiles(string jumbleLetters)
//    {
//        foreach (char letter in jumbleLetters)
//        {
//            GameObject tileObject = Instantiate(tilePrefab, tileTray);
//            PuzzleTile tile = tileObject.GetComponent<PuzzleTile>();
//            tile.Setup(letter, this);
//        }
//    }
//    #endregion

//    #region Tile Placement
//    public void OnTileTapped(PuzzleTile tile)
//    {
//        AnswerSlot nextEmptySlot = FindNextEmptySlot();
//        if (nextEmptySlot == null) return;

//        nextEmptySlot.PlaceLetter(tile.Letter);
//        Destroy(tile.gameObject);

//        if (AllSlotsFilled())
//        {
//            CheckAnswer();
//        }
//    }

//    AnswerSlot FindNextEmptySlot()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return slot;
//            }
//        }
//        return null;
//    }

//    bool AllSlotsFilled()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//    #endregion

//    #region Answer Checking
//    void CheckAnswer()
//    {
//        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);

//        for (int i = 0; i < currentSlots.Count; i++)
//        {
//            char correctLetter = correctAnswer[i];
//            char placedLetter = currentSlots[i].GetLetter();

//            if (placedLetter == correctLetter)
//            {
//                currentSlots[i].ShowCorrect();
//            }
//            else
//            {
//                currentSlots[i].ShowWrong();
//            }
//        }
//    }
//    #endregion
//}
#endregion

#region Phase 1 Sprint 6 - GameSession (level number display)
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections.Generic;

//public class GameSession : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] PuzzleLoader loader;
//    [SerializeField] Image puzzleImageDisplay;
//    [SerializeField] Transform tileTray;
//    [SerializeField] Transform answerSlotRow;
//    [SerializeField] GameObject tilePrefab;
//    [SerializeField] GameObject slotPrefab;
//    [SerializeField] TMP_Text levelText;
//    #endregion

//    #region State
//    int currentIndex = 0;
//    List<AnswerSlot> currentSlots = new List<AnswerSlot>();
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        LoadPuzzle();
//    }
//    #endregion

//    #region Puzzle Navigation
//    public PuzzleData GetCurrentPuzzle()
//    {
//        return loader.puzzles[currentIndex];
//    }

//    public bool HasNextPuzzle()
//    {
//        return currentIndex + 1 < loader.puzzles.Length;
//    }

//    public void LoadNextPuzzle()
//    {
//        if (HasNextPuzzle())
//        {
//            currentIndex++;
//            LoadPuzzle();
//        }
//    }
//    #endregion

//    #region Puzzle Setup
//    void LoadPuzzle()
//    {
//        ClearTray();
//        ClearSlots();

//        PuzzleData puzzle = GetCurrentPuzzle();

//        puzzleImageDisplay.sprite = loader.GetSpriteForPuzzle(puzzle);
//        UpdateLevelText(puzzle);

//        SpawnSlots(puzzle.answerWords);
//        SpawnTiles(puzzle.jumbleLetters);
//    }

//    void UpdateLevelText(PuzzleData puzzle)
//    {
//        if (levelText != null)
//        {
//            levelText.text = $"Level {puzzle.levelNumber}";
//        }
//    }

//    void ClearTray()
//    {
//        foreach (Transform child in tileTray)
//        {
//            Destroy(child.gameObject);
//        }
//    }

//    void ClearSlots()
//    {
//        foreach (Transform child in answerSlotRow)
//        {
//            Destroy(child.gameObject);
//        }
//        currentSlots.Clear();
//    }

//    void SpawnSlots(string[] answerWords)
//    {
//        for (int w = 0; w < answerWords.Length; w++)
//        {
//            string word = answerWords[w];

//            for (int i = 0; i < word.Length; i++)
//            {
//                GameObject slotObject = Instantiate(slotPrefab, answerSlotRow);
//                AnswerSlot slot = slotObject.GetComponent<AnswerSlot>();
//                currentSlots.Add(slot);
//            }

//            bool isLastWord = (w == answerWords.Length - 1);
//            if (!isLastWord)
//            {
//                SpawnWordGap();
//            }
//        }
//    }

//    void SpawnWordGap()
//    {
//        GameObject gap = new GameObject("WordGap", typeof(RectTransform));
//        gap.transform.SetParent(answerSlotRow, false);
//        LayoutElement layoutElement = gap.AddComponent<LayoutElement>();
//        layoutElement.minWidth = 40f;
//    }

//    void SpawnTiles(string jumbleLetters)
//    {
//        foreach (char letter in jumbleLetters)
//        {
//            GameObject tileObject = Instantiate(tilePrefab, tileTray);
//            PuzzleTile tile = tileObject.GetComponent<PuzzleTile>();
//            tile.Setup(letter, this);
//        }
//    }
//    #endregion

//    #region Tile Placement
//    public void OnTileTapped(PuzzleTile tile)
//    {
//        AnswerSlot nextEmptySlot = FindNextEmptySlot();
//        if (nextEmptySlot == null) return;

//        nextEmptySlot.PlaceLetter(tile.Letter);
//        Destroy(tile.gameObject);

//        if (AllSlotsFilled())
//        {
//            CheckAnswer();
//        }
//    }

//    AnswerSlot FindNextEmptySlot()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return slot;
//            }
//        }
//        return null;
//    }

//    bool AllSlotsFilled()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//    #endregion

//    #region Answer Checking
//    void CheckAnswer()
//    {
//        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);

//        for (int i = 0; i < currentSlots.Count; i++)
//        {
//            char correctLetter = correctAnswer[i];
//            char placedLetter = currentSlots[i].GetLetter();

//            if (placedLetter == correctLetter)
//            {
//                currentSlots[i].ShowCorrect();
//            }
//            else
//            {
//                currentSlots[i].ShowWrong();
//            }
//        }
//    }
//    #endregion
//}
#endregion

#region Phase 1 Sprint 7 - GameSession (level text + Hint/Clear/Undo/Scramble support)
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections.Generic;

//public class GameSession : MonoBehaviour
//{
//    #region Configuration
//    [Header("Puzzle Data")]
//    [SerializeField] PuzzleLoader loader;

//    [Header("Puzzle Display")]
//    [SerializeField] Image puzzleImageDisplay;
//    [SerializeField] Transform tileTray;
//    [SerializeField] Transform answerSlotRow;
//    [SerializeField] GameObject tilePrefab;
//    [SerializeField] GameObject slotPrefab;
//    [SerializeField] TMP_Text levelText;
//    #endregion

//    #region State
//    int currentIndex = 0;
//    List<AnswerSlot> currentSlots = new List<AnswerSlot>();
//    List<PuzzleTile> currentTiles = new List<PuzzleTile>();
//    Stack<(AnswerSlot slot, char letter)> placementHistory = new Stack<(AnswerSlot, char)>();
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        LoadPuzzle();
//    }
//    #endregion

//    #region Puzzle Navigation
//    public PuzzleData GetCurrentPuzzle()
//    {
//        return loader.puzzles[currentIndex];
//    }

//    public bool HasNextPuzzle()
//    {
//        return currentIndex + 1 < loader.puzzles.Length;
//    }

//    public void LoadNextPuzzle()
//    {
//        if (HasNextPuzzle())
//        {
//            currentIndex++;
//            LoadPuzzle();
//        }
//    }
//    #endregion

//    #region Puzzle Setup
//    void LoadPuzzle()
//    {
//        ClearTray();
//        ClearSlots();
//        placementHistory.Clear();

//        PuzzleData puzzle = GetCurrentPuzzle();

//        puzzleImageDisplay.sprite = loader.GetSpriteForPuzzle(puzzle);
//        UpdateLevelText(puzzle);
//        feedbackBanner.SetActive(false);

//        SpawnSlots(puzzle.answerWords);
//        SpawnTiles(puzzle.jumbleLetters);
//    }

//    void UpdateLevelText(PuzzleData puzzle)
//    {
//        if (levelText != null)
//        {
//            levelText.text = $"Level {puzzle.levelNumber}";
//        }
//    }

//    void ClearTray()
//    {
//        foreach (Transform child in tileTray)
//        {
//            Destroy(child.gameObject);
//        }
//        currentTiles.Clear();
//    }

//    void ClearSlots()
//    {
//        foreach (Transform child in answerSlotRow)
//        {
//            Destroy(child.gameObject);
//        }
//        currentSlots.Clear();
//    }

//    void SpawnSlots(string[] answerWords)
//    {
//        for (int w = 0; w < answerWords.Length; w++)
//        {
//            string word = answerWords[w];

//            for (int i = 0; i < word.Length; i++)
//            {
//                GameObject slotObject = Instantiate(slotPrefab, answerSlotRow);
//                AnswerSlot slot = slotObject.GetComponent<AnswerSlot>();
//                currentSlots.Add(slot);
//            }

//            bool isLastWord = (w == answerWords.Length - 1);
//            if (!isLastWord)
//            {
//                SpawnWordGap();
//            }
//        }
//    }

//    void SpawnWordGap()
//    {
//        GameObject gap = new GameObject("WordGap", typeof(RectTransform));
//        gap.transform.SetParent(answerSlotRow, false);
//        LayoutElement layoutElement = gap.AddComponent<LayoutElement>();
//        layoutElement.minWidth = 40f;
//    }

//    void SpawnTiles(string jumbleLetters)
//    {
//        foreach (char letter in jumbleLetters)
//        {
//            SpawnSingleTile(letter);
//        }
//    }

//    PuzzleTile SpawnSingleTile(char letter)
//    {
//        GameObject tileObject = Instantiate(tilePrefab, tileTray);
//        PuzzleTile tile = tileObject.GetComponent<PuzzleTile>();
//        tile.Setup(letter, this);
//        currentTiles.Add(tile);
//        return tile;
//    }
//    #endregion

//    #region Tile Placement
//    public void OnTileTapped(PuzzleTile tile)
//    {
//        PlaceTile(tile);
//    }

//    void PlaceTile(PuzzleTile tile)
//    {
//        AnswerSlot nextEmptySlot = FindNextEmptySlot();
//        if (nextEmptySlot == null) return;

//        char letter = tile.Letter;

//        nextEmptySlot.PlaceLetter(letter);
//        placementHistory.Push((nextEmptySlot, letter));

//        currentTiles.Remove(tile);
//        Destroy(tile.gameObject);

//        if (AllSlotsFilled())
//        {
//            CheckAnswer();
//        }
//    }

//    AnswerSlot FindNextEmptySlot()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return slot;
//            }
//        }
//        return null;
//    }

//    bool AllSlotsFilled()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//    #endregion

//    #region Feedback Banner
//    [Header("Feedback Banner")]
//    [SerializeField] GameObject feedbackBanner;
//    [SerializeField] GameObject correctImage;
//    [SerializeField] GameObject tryAgainImage;
//    #endregion

//    #region Answer Checking
//    void CheckAnswer()
//    {
//        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);
//        bool allCorrect = true;

//        for (int i = 0; i < currentSlots.Count; i++)
//        {
//            char correctLetter = correctAnswer[i];
//            char placedLetter = currentSlots[i].GetLetter();

//            if (placedLetter == correctLetter)
//            {
//                currentSlots[i].ShowCorrect();
//            }
//            else
//            {
//                currentSlots[i].ShowWrong();
//                allCorrect = false;
//            }
//        }

//        ShowFeedback(allCorrect);
//    }

//    void ShowFeedback(bool wasCorrect)
//    {
//        feedbackBanner.SetActive(true);
//        correctImage.SetActive(wasCorrect);
//        tryAgainImage.SetActive(!wasCorrect);
//    }
//    #endregion

//    #region HUD Actions
//    public void ClearAnswer()
//    {
//        LoadPuzzle();
//    }

//    public void UndoLastPlacement()
//    {
//        if (placementHistory.Count == 0) return;

//        var (slot, letter) = placementHistory.Pop();
//        slot.ShowEmpty();
//        SpawnSingleTile(letter);
//    }

//    public void GiveHint()
//    {
//        AnswerSlot nextEmptySlot = FindNextEmptySlot();
//        if (nextEmptySlot == null) return;

//        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);
//        int slotIndex = currentSlots.IndexOf(nextEmptySlot);
//        char neededLetter = correctAnswer[slotIndex];

//        PuzzleTile matchingTile = currentTiles.Find(t => t.Letter == neededLetter);
//        if (matchingTile != null)
//        {
//            PlaceTile(matchingTile);
//        }
//    }

//    public void ScrambleRemaining()
//    {
//        List<PuzzleTile> shuffled = new List<PuzzleTile>(currentTiles);

//        for (int i = shuffled.Count - 1; i > 0; i--)
//        {
//            int randomIndex = Random.Range(0, i + 1);
//            (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
//        }

//        for (int i = 0; i < shuffled.Count; i++)
//        {
//            shuffled[i].transform.SetSiblingIndex(i);
//        }
//    }
//    #endregion
//}
#endregion

#region Phase 1 Sprint 7 - GameSession (level text + Hint/Clear/Undo/Scramble support)
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections.Generic;

//public class GameSession : MonoBehaviour
//{
//    #region Configuration
//    [Header("Puzzle Data")]
//    [SerializeField] PuzzleLoader loader;

//    [Header("Puzzle Display")]
//    [SerializeField] Image puzzleImageDisplay;
//    [SerializeField] Transform tileTray;
//    [SerializeField] Transform answerSlotRow;
//    [SerializeField] GameObject tilePrefab;
//    [SerializeField] GameObject slotPrefab;
//    [SerializeField] TMP_Text levelText;
//    #endregion

//    #region State
//    int currentIndex = 0;
//    List<AnswerSlot> currentSlots = new List<AnswerSlot>();
//    List<PuzzleTile> currentTiles = new List<PuzzleTile>();
//    Stack<(AnswerSlot slot, char letter)> placementHistory = new Stack<(AnswerSlot, char)>();
//    #endregion

//    #region Timer
//    [Header("Timer")]
//    [SerializeField] TMP_Text timerText;
//    [SerializeField] float startingTimeSeconds = 60f;

//    float timeRemaining;
//    bool isTimerRunning;
//    #endregion

//    #region Level End
//    [Header("Level End")]
//    [SerializeField] GameObject levelFailedPanel;
//    [SerializeField] GameObject levelPassedPanel;
//    #endregion

//    #region Unity Lifecycle
//    void Start()
//    {
//        LoadPuzzle();
//    }

//    void Update()
//    {
//        if (!isTimerRunning) return;

//        timeRemaining -= Time.deltaTime;

//        if (timeRemaining <= 0f)
//        {
//            timeRemaining = 0f;
//            isTimerRunning = false;
//            ShowLevelFailed();
//        }

//        UpdateTimerText();
//    }
//    #endregion

//    #region Timer Logic
//    void StartTimer()
//    {
//        timeRemaining = startingTimeSeconds;
//        isTimerRunning = true;
//        UpdateTimerText();
//    }

//    void UpdateTimerText()
//    {
//        if (timerText == null) return;

//        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
//        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
//        timerText.text = $"{minutes:00}:{seconds:00}";
//    }
//    #endregion

//    #region Level End Actions
//    void ShowLevelFailed()
//    {
//        if (levelFailedPanel != null)
//        {
//            levelFailedPanel.SetActive(true);
//        }
//    }

//    void ShowLevelPassed()
//    {
//        if (levelPassedPanel != null)
//        {
//            levelPassedPanel.SetActive(true);
//        }
//    }
//    #endregion

//    #region Puzzle Navigation
//    public PuzzleData GetCurrentPuzzle()
//    {
//        return loader.puzzles[currentIndex];
//    }

//    public bool HasNextPuzzle()
//    {
//        return currentIndex + 1 < loader.puzzles.Length;
//    }

//    public void LoadNextPuzzle()
//    {
//        if (HasNextPuzzle())
//        {
//            currentIndex++;
//            LoadPuzzle();
//        }
//    }
//    #endregion

//    #region Puzzle Setup
//    void LoadPuzzle()
//    {
//        ClearTray();
//        ClearSlots();
//        placementHistory.Clear();

//        PuzzleData puzzle = GetCurrentPuzzle();

//        puzzleImageDisplay.sprite = loader.GetSpriteForPuzzle(puzzle);
//        UpdateLevelText(puzzle);
//        feedbackBanner.SetActive(false);
//        StartTimer();

//        SpawnSlots(puzzle.answerWords);
//        SpawnTiles(puzzle.jumbleLetters);
//    }

//    void UpdateLevelText(PuzzleData puzzle)
//    {
//        if (levelText != null)
//        {
//            levelText.text = $"Level {puzzle.levelNumber}";
//        }
//    }

//    void ClearTray()
//    {
//        foreach (Transform child in tileTray)
//        {
//            Destroy(child.gameObject);
//        }
//        currentTiles.Clear();
//    }

//    void ClearSlots()
//    {
//        foreach (Transform child in answerSlotRow)
//        {
//            Destroy(child.gameObject);
//        }
//        currentSlots.Clear();
//    }

//    void SpawnSlots(string[] answerWords)
//    {
//        for (int w = 0; w < answerWords.Length; w++)
//        {
//            string word = answerWords[w];

//            for (int i = 0; i < word.Length; i++)
//            {
//                GameObject slotObject = Instantiate(slotPrefab, answerSlotRow);
//                AnswerSlot slot = slotObject.GetComponent<AnswerSlot>();
//                currentSlots.Add(slot);
//            }

//            bool isLastWord = (w == answerWords.Length - 1);
//            if (!isLastWord)
//            {
//                SpawnWordGap();
//            }
//        }
//    }

//    void SpawnWordGap()
//    {
//        GameObject gap = new GameObject("WordGap", typeof(RectTransform));
//        gap.transform.SetParent(answerSlotRow, false);
//        LayoutElement layoutElement = gap.AddComponent<LayoutElement>();
//        layoutElement.minWidth = 40f;
//    }

//    void SpawnTiles(string jumbleLetters)
//    {
//        foreach (char letter in jumbleLetters)
//        {
//            SpawnSingleTile(letter);
//        }
//    }

//    PuzzleTile SpawnSingleTile(char letter)
//    {
//        GameObject tileObject = Instantiate(tilePrefab, tileTray);
//        PuzzleTile tile = tileObject.GetComponent<PuzzleTile>();
//        tile.Setup(letter, this);
//        currentTiles.Add(tile);
//        return tile;
//    }
//    #endregion

//    #region Tile Placement
//    public void OnTileTapped(PuzzleTile tile)
//    {
//        PlaceTile(tile);
//    }

//    void PlaceTile(PuzzleTile tile)
//    {
//        AnswerSlot nextEmptySlot = FindNextEmptySlot();
//        if (nextEmptySlot == null) return;

//        char letter = tile.Letter;

//        nextEmptySlot.PlaceLetter(letter);
//        placementHistory.Push((nextEmptySlot, letter));

//        currentTiles.Remove(tile);
//        Destroy(tile.gameObject);

//        if (AllSlotsFilled())
//        {
//            CheckAnswer();
//        }
//    }

//    AnswerSlot FindNextEmptySlot()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return slot;
//            }
//        }
//        return null;
//    }

//    bool AllSlotsFilled()
//    {
//        foreach (AnswerSlot slot in currentSlots)
//        {
//            if (!slot.IsFilled)
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//    #endregion

//    #region Feedback Banner
//    [Header("Feedback Banner")]
//    [SerializeField] GameObject feedbackBanner;
//    [SerializeField] GameObject correctImage;
//    [SerializeField] GameObject tryAgainImage;
//    #endregion

//    #region Answer Checking
//    void CheckAnswer()
//    {
//        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);
//        bool allCorrect = true;

//        for (int i = 0; i < currentSlots.Count; i++)
//        {
//            char correctLetter = correctAnswer[i];
//            char placedLetter = currentSlots[i].GetLetter();

//            if (placedLetter == correctLetter)
//            {
//                currentSlots[i].ShowCorrect();
//            }
//            else
//            {
//                currentSlots[i].ShowWrong();
//                allCorrect = false;
//            }
//        }

//        ShowFeedback(allCorrect);

//        if (allCorrect)
//        {
//            isTimerRunning = false;
//            ShowLevelPassed();
//        }
//    }

//    void ShowFeedback(bool wasCorrect)
//    {
//        feedbackBanner.SetActive(true);
//        correctImage.SetActive(wasCorrect);
//        tryAgainImage.SetActive(!wasCorrect);
//    }
//    #endregion

//    #region HUD Actions
//    public void ClearAnswer()
//    {
//        LoadPuzzle();
//    }

//    public void UndoLastPlacement()
//    {
//        if (placementHistory.Count == 0) return;

//        var (slot, letter) = placementHistory.Pop();
//        slot.ShowEmpty();
//        SpawnSingleTile(letter);
//    }

//    public void GiveHint()
//    {
//        AnswerSlot nextEmptySlot = FindNextEmptySlot();
//        if (nextEmptySlot == null) return;

//        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);
//        int slotIndex = currentSlots.IndexOf(nextEmptySlot);
//        char neededLetter = correctAnswer[slotIndex];

//        PuzzleTile matchingTile = currentTiles.Find(t => t.Letter == neededLetter);
//        if (matchingTile != null)
//        {
//            PlaceTile(matchingTile);
//        }
//    }

//    public void ScrambleRemaining()
//    {
//        List<PuzzleTile> shuffled = new List<PuzzleTile>(currentTiles);

//        for (int i = shuffled.Count - 1; i > 0; i--)
//        {
//            int randomIndex = Random.Range(0, i + 1);
//            (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
//        }

//        for (int i = 0; i < shuffled.Count; i++)
//        {
//            shuffled[i].transform.SetSiblingIndex(i);
//        }
//    }
//    #endregion
//}
#endregion

#region Phase 1 Sprint 7 - GameSession (level text + Hint/Clear/Undo/Scramble support)
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameSession : MonoBehaviour
{
    #region Configuration
    [Header("Puzzle Data")]
    [SerializeField] PuzzleLoader loader;

    [Header("Puzzle Display")]
    [SerializeField] Image puzzleImageDisplay;
    [SerializeField] Transform tileTray;
    [SerializeField] Transform answerSlotRow;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] TMP_Text levelText;
    #endregion

    #region State
    int currentIndex = 0;
    List<AnswerSlot> currentSlots = new List<AnswerSlot>();
    List<PuzzleTile> currentTiles = new List<PuzzleTile>();
    Stack<(AnswerSlot slot, char letter)> placementHistory = new Stack<(AnswerSlot, char)>();

    bool usedHint;
    bool usedUndo;
    bool usedScramble;
    #endregion

    #region Timer
    [Header("Timer")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] float startingTimeSeconds = 60f;

    float timeRemaining;
    bool isTimerRunning;
    #endregion

    #region Level End
    [Header("Level End")]
    [SerializeField] GameObject levelFailedPanel;
    [SerializeField] GameObject levelPassedPanel;

    [Header("Level Passed Details")]
    [SerializeField] Image[] starIcons;
    [SerializeField] Sprite starFilledSprite;
    [SerializeField] Sprite starEmptySprite;
    [SerializeField] GameObject hintBonusPill;
    #endregion

    #region Unity Lifecycle
    void Start()
    {
        loader.LoadCategory(SelectedLevel.categoryIndex);

        currentIndex = System.Array.FindIndex(loader.puzzles, p => p.levelNumber == SelectedLevel.levelNumber);
        if (currentIndex < 0) currentIndex = 0;

        LoadPuzzle();
    }

    void Update()
    {
        if (!isTimerRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isTimerRunning = false;
            ShowLevelFailed();
        }

        UpdateTimerText();
    }
    #endregion

    #region Timer Logic
    void StartTimer()
    {
        timeRemaining = startingTimeSeconds;
        isTimerRunning = true;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    #endregion

    #region Level End Actions
    void ShowLevelFailed()
    {
        if (levelFailedPanel != null)
        {
            levelFailedPanel.SetActive(true);
        }
    }

    void ShowLevelPassed()
    {
        if (levelPassedPanel != null)
        {
            levelPassedPanel.SetActive(true);
        }

        bool usedAnyAid = usedHint || usedUndo || usedScramble;
        int starCount = usedAnyAid ? 2 : 3;

        PuzzleData puzzle = GetCurrentPuzzle();
        ProgressManager.SetStars(puzzle.category, puzzle.levelNumber, starCount);

        UpdateStarDisplay(starCount);
        UpdateHintBonusDisplay();
    }

    void UpdateStarDisplay(int filledCount)
    {
        for (int i = 0; i < starIcons.Length; i++)
        {
            bool isFilled = i < filledCount;
            starIcons[i].sprite = isFilled ? starFilledSprite : starEmptySprite;
        }
    }

    void UpdateHintBonusDisplay()
    {
        if (hintBonusPill != null)
        {
            hintBonusPill.SetActive(!usedHint);
        }
    }

    void HideLevelEndPanels()
    {
        if (levelFailedPanel != null) levelFailedPanel.SetActive(false);
        if (levelPassedPanel != null) levelPassedPanel.SetActive(false);
    }
    #endregion

    #region Puzzle Navigation
    public PuzzleData GetCurrentPuzzle()
    {
        return loader.puzzles[currentIndex];
    }

    public bool HasNextPuzzle()
    {
        return currentIndex + 1 < loader.puzzles.Length;
    }

    public void LoadNextPuzzle()
    {
        if (HasNextPuzzle())
        {
            currentIndex++;
            LoadPuzzle();
        }
    }
    #endregion

    #region Puzzle Setup
    void LoadPuzzle()
    {
        ClearTray();
        ClearSlots();
        placementHistory.Clear();
        usedHint = false;
        usedUndo = false;
        usedScramble = false;

        PuzzleData puzzle = GetCurrentPuzzle();

        puzzleImageDisplay.sprite = loader.GetSpriteForPuzzle(puzzle);
        UpdateLevelText(puzzle);
        feedbackBanner.SetActive(false);
        HideLevelEndPanels();
        StartTimer();

        SpawnSlots(puzzle.answerWords);
        SpawnTiles(puzzle.jumbleLetters);
    }

    void UpdateLevelText(PuzzleData puzzle)
    {
        if (levelText != null)
        {
            levelText.text = $"Level {puzzle.levelNumber}";
        }
    }

    void ClearTray()
    {
        foreach (Transform child in tileTray)
        {
            Destroy(child.gameObject);
        }
        currentTiles.Clear();
    }

    void ClearSlots()
    {
        foreach (Transform child in answerSlotRow)
        {
            Destroy(child.gameObject);
        }
        currentSlots.Clear();
    }

    void SpawnSlots(string[] answerWords)
    {
        for (int w = 0; w < answerWords.Length; w++)
        {
            string word = answerWords[w];

            for (int i = 0; i < word.Length; i++)
            {
                GameObject slotObject = Instantiate(slotPrefab, answerSlotRow);
                AnswerSlot slot = slotObject.GetComponent<AnswerSlot>();
                currentSlots.Add(slot);
            }

            bool isLastWord = (w == answerWords.Length - 1);
            if (!isLastWord)
            {
                SpawnWordGap();
            }
        }
    }

    void SpawnWordGap()
    {
        GameObject gap = new GameObject("WordGap", typeof(RectTransform));
        gap.transform.SetParent(answerSlotRow, false);
        LayoutElement layoutElement = gap.AddComponent<LayoutElement>();
        layoutElement.minWidth = 40f;
    }

    void SpawnTiles(string jumbleLetters)
    {
        foreach (char letter in jumbleLetters)
        {
            SpawnSingleTile(letter);
        }
    }

    PuzzleTile SpawnSingleTile(char letter)
    {
        GameObject tileObject = Instantiate(tilePrefab, tileTray);
        PuzzleTile tile = tileObject.GetComponent<PuzzleTile>();
        tile.Setup(letter, this);
        currentTiles.Add(tile);
        return tile;
    }
    #endregion

    #region Tile Placement
    public void OnTileTapped(PuzzleTile tile)
    {
        PlaceTile(tile);
    }

    void PlaceTile(PuzzleTile tile)
    {
        AnswerSlot nextEmptySlot = FindNextEmptySlot();
        if (nextEmptySlot == null) return;

        char letter = tile.Letter;

        nextEmptySlot.PlaceLetter(letter);
        placementHistory.Push((nextEmptySlot, letter));

        currentTiles.Remove(tile);
        Destroy(tile.gameObject);

        if (AllSlotsFilled())
        {
            CheckAnswer();
        }
    }

    AnswerSlot FindNextEmptySlot()
    {
        foreach (AnswerSlot slot in currentSlots)
        {
            if (!slot.IsFilled)
            {
                return slot;
            }
        }
        return null;
    }

    bool AllSlotsFilled()
    {
        foreach (AnswerSlot slot in currentSlots)
        {
            if (!slot.IsFilled)
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Feedback Banner
    [Header("Feedback Banner")]
    [SerializeField] GameObject feedbackBanner;
    [SerializeField] GameObject correctImage;
    [SerializeField] GameObject tryAgainImage;
    #endregion

    #region Answer Checking
    void CheckAnswer()
    {
        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);
        bool allCorrect = true;

        for (int i = 0; i < currentSlots.Count; i++)
        {
            char correctLetter = correctAnswer[i];
            char placedLetter = currentSlots[i].GetLetter();

            if (placedLetter == correctLetter)
            {
                currentSlots[i].ShowCorrect();
            }
            else
            {
                currentSlots[i].ShowWrong();
                allCorrect = false;
            }
        }

        ShowFeedback(allCorrect);

        if (allCorrect)
        {
            isTimerRunning = false;
            ShowLevelPassed();
        }
    }

    void ShowFeedback(bool wasCorrect)
    {
        feedbackBanner.SetActive(true);
        correctImage.SetActive(wasCorrect);
        tryAgainImage.SetActive(!wasCorrect);
    }
    #endregion

    #region HUD Actions
    public void ClearAnswer()
    {
        LoadPuzzle();
    }

    public void UndoLastPlacement()
    {
        if (placementHistory.Count == 0) return;

        usedUndo = true;
        var (slot, letter) = placementHistory.Pop();
        slot.ShowEmpty();
        SpawnSingleTile(letter);
    }

    public void GiveHint()
    {
        AnswerSlot nextEmptySlot = FindNextEmptySlot();
        if (nextEmptySlot == null) return;

        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);
        int slotIndex = currentSlots.IndexOf(nextEmptySlot);
        char neededLetter = correctAnswer[slotIndex];

        PuzzleTile matchingTile = currentTiles.Find(t => t.Letter == neededLetter);
        if (matchingTile != null)
        {
            usedHint = true;
            PlaceTile(matchingTile);
        }
    }

    public void ScrambleRemaining()
    {
        usedScramble = true;
        List<PuzzleTile> shuffled = new List<PuzzleTile>(currentTiles);

        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
        }

        for (int i = 0; i < shuffled.Count; i++)
        {
            shuffled[i].transform.SetSiblingIndex(i);
        }
    }
    #endregion
}
#endregion