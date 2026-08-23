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
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameSession : MonoBehaviour
{
    #region Configuration
    [SerializeField] PuzzleLoader loader;
    [SerializeField] Image puzzleImageDisplay;
    [SerializeField] Transform tileTray;
    [SerializeField] Transform answerSlotRow;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject slotPrefab;
    #endregion

    #region State
    int currentIndex = 0;
    List<AnswerSlot> currentSlots = new List<AnswerSlot>();
    #endregion

    #region Unity Lifecycle
    void Start()
    {
        LoadPuzzle();
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

        PuzzleData puzzle = GetCurrentPuzzle();

        puzzleImageDisplay.sprite = loader.GetSpriteForPuzzle(puzzle);

        SpawnSlots(puzzle.answerWords);
        SpawnTiles(puzzle.jumbleLetters);
    }

    void ClearTray()
    {
        foreach (Transform child in tileTray)
        {
            Destroy(child.gameObject);
        }
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
            GameObject tileObject = Instantiate(tilePrefab, tileTray);
            PuzzleTile tile = tileObject.GetComponent<PuzzleTile>();
            tile.Setup(letter, this);
        }
    }
    #endregion

    #region Tile Placement
    public void OnTileTapped(PuzzleTile tile)
    {
        AnswerSlot nextEmptySlot = FindNextEmptySlot();
        if (nextEmptySlot == null) return;

        nextEmptySlot.PlaceLetter(tile.Letter);
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

    #region Answer Checking
    void CheckAnswer()
    {
        string correctAnswer = string.Join("", GetCurrentPuzzle().answerWords);

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
            }
        }
    }
    #endregion
}
#endregion

