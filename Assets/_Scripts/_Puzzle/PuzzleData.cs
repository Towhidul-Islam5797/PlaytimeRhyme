#region Summary
/// <summary>
/// This class represents the data structure for a puzzle in the game. It contains information about the puzzle's category, level number, answer words,
/// jumbled letters, and the associated image file name.
/// 
/// Usage:
/// 1. Create an instance of PuzzleData to represent a specific puzzle.
/// 2. Populate the fields with the appropriate data for that puzzle.
/// 3. Use the PuzzleData instance in conjunction with other game components, such as PuzzleLoader and GameSession, to manage and display puzzles in the game.
/// 4. Ensure that the image file name corresponds to an actual image asset in the project for proper display.
/// Note: This class is marked as [System.Serializable] to allow it to be serialized and displayed in the Unity Inspector.
/// </summary>
#endregion

#region Phase 1 Sprint 1 - PuzzleData Structure
[System.Serializable]
public class PuzzleData
{
    public string category;
    public int levelNumber;
    public string[] answerWords;
    public string jumbleLetters;
    public string imageFileName;
}
#endregion