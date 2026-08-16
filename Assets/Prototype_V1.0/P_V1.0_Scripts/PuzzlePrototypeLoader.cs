using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PuzzlePrototypeLoader : MonoBehaviour
{
    [SerializeField] string folderPath = "Assets/Prototype_V1.0/P_V1.0_Resources/Sprites/Couplets";

    public PuzzleData[] puzzles;

    void Awake()
    {
#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
        puzzles = new PuzzleData[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            puzzles[i] = ParseFileName(fileName, sprite);
        }
#else
        Debug.LogWarning("This prototype loader only works in the Unity Editor, not in a build.");
        puzzles = new PuzzleData[0];
#endif
    }

    PuzzleData ParseFileName(string fileName, Sprite sprite)
    {
        string[] parts = fileName.Split(new string[] { ". " }, System.StringSplitOptions.None);
        string answerPart = parts[0];
        string levelPart = parts[1];

        PuzzleData data = new PuzzleData();
        data.category = "Couplets";
        data.levelNumber = int.Parse(levelPart);
        data.answerWords = answerPart.Split(' ');
        data.imageFileName = fileName;
        data.image = sprite;

        return data;
    }
}