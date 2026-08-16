using UnityEngine;

public class PuzzlePrototypeLoader : MonoBehaviour
{
    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Couplets");
        PuzzleData[] puzzles = new PuzzleData[sprites.Length];

        for (int i = 0; i < sprites.Length; i++)
        {
            puzzles[i] = ParseFileName(sprites[i].name);
        }

        foreach (PuzzleData puzzle in puzzles)
        {
            Debug.Log($"Level {puzzle.levelNumber}: {string.Join(", ", puzzle.answerWords)}");
        }
    }

    PuzzleData ParseFileName(string fileName)
    {
        string[] parts = fileName.Split(new string[] { ". " }, System.StringSplitOptions.None);
        string answerPart = parts[0];
        string levelPart = parts[1];

        PuzzleData data = new PuzzleData();
        data.category = "Couplets";
        data.levelNumber = int.Parse(levelPart);
        data.answerWords = answerPart.Split(' ');
        data.imageFileName = fileName;

        return data;
    }
}