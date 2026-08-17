using UnityEngine;
using UnityEngine.UI;

public class PuzzleViewer : MonoBehaviour
{
    [SerializeField] PuzzlePrototypeLoader loader;
    [SerializeField] Image puzzleImageDisplay;

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
        P_PuzzleData puzzle = loader.puzzles[index];
        puzzleImageDisplay.sprite = puzzle.image;
        Debug.Log($"Showing Level {puzzle.levelNumber}: {string.Join(", ", puzzle.answerWords)}");
    }
}