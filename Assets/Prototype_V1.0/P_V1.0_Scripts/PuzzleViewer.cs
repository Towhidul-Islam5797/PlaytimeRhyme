#region v1
//using UnityEngine;
//using UnityEngine.UI;

//public class PuzzleViewer : MonoBehaviour
//{
//    [SerializeField] PuzzlePrototypeLoader loader;
//    [SerializeField] Image puzzleImageDisplay;

//    int currentIndex = 0;

//    void Start()
//    {
//        ShowPuzzle(currentIndex);
//    }

//    public void ShowNext()
//    {
//        currentIndex++;
//        if (currentIndex >= loader.puzzles.Length)
//        {
//            currentIndex = 0;
//        }
//        ShowPuzzle(currentIndex);
//    }

//    void ShowPuzzle(int index)
//    {
//        P_PuzzleData puzzle = loader.puzzles[index];
//        puzzleImageDisplay.sprite = puzzle.image;
//        Debug.Log($"Showing Level {puzzle.levelNumber}: {string.Join(", ", puzzle.answerWords)}");
//    }
//}
#endregion

#region V2
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleViewer : MonoBehaviour
{
    [SerializeField] PuzzlePrototypeLoader loader;
    [SerializeField] Image puzzleImageDisplay;
    [SerializeField] TMP_Text jumbleText;
    [SerializeField] TMP_Text levelText;

    int currentIndex = 0;

    void Start()
    {
        ShowPuzzle(currentIndex);
    }

    public void ShowNext()
    {
        currentIndex++;
        if (currentIndex >= loader.puzzles.Length)
        {
            currentIndex = 0;
        }
        ShowPuzzle(currentIndex);
    }

    void ShowPuzzle(int index)
    {
        PuzzleData puzzle = loader.puzzles[index];
        puzzleImageDisplay.sprite = loader.GetSpriteForPuzzle(puzzle);
        jumbleText.text = puzzle.jumbleLetters;
        levelText.text = $"Level {puzzle.levelNumber} ({index + 1}/{loader.puzzles.Length})";

        Debug.Log($"Level {puzzle.levelNumber}: {string.Join(", ", puzzle.answerWords)} -> {puzzle.jumbleLetters}");
    }
}
#endregion