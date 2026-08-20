#region v1
//using UnityEngine;
//using System.Collections.Generic;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//public class PuzzlePrototypeLoader : MonoBehaviour
//{
//    [SerializeField] string folderPath = "Assets/Prototype_V1.0/P_V1.0_Resources/Sprites/Couplets";

//    public P_PuzzleData[] puzzles;

//    void Awake()
//    {
//#if UNITY_EDITOR
//        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
//        puzzles = new P_PuzzleData[guids.Length];

//        for (int i = 0; i < guids.Length; i++)
//        {
//            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
//            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
//            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
//            puzzles[i] = ParseFileName(fileName, sprite);
//        }
//#else
//        Debug.LogWarning("This prototype loader only works in the Unity Editor, not in a build.");
//        puzzles = new PuzzleData[0];
//#endif
//    }

//    P_PuzzleData ParseFileName(string fileName, Sprite sprite)
//    {
//        string[] parts = fileName.Split(new string[] { ". " }, System.StringSplitOptions.None);
//        string answerPart = parts[0];
//        string levelPart = parts[1];

//        P_PuzzleData data = new P_PuzzleData();
//        data.category = "Couplets";
//        data.levelNumber = int.Parse(levelPart);
//        data.answerWords = answerPart.Split(' ');
//        data.imageFileName = fileName;
//        data.image = sprite;

//        return data;
//    }
//}
#endregion

#region v2
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PuzzlePrototypeLoader : MonoBehaviour
{
    [SerializeField] string folderPath = "Assets/_Import/Level_Easy/Rhyme Couplets Saga";
    [SerializeField] string csvPath = "Assets/_Import/Level_Easy/Rhyme Couplets Saga/Data/Couplets.csv";

    public PuzzleData[] puzzles;
    Dictionary<string, Sprite> spritesByFileName = new Dictionary<string, Sprite>();

    void Awake()
    {
#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            string fileNameWithExtension = System.IO.Path.GetFileName(path);
            spritesByFileName[fileNameWithExtension] = sprite;
        }

        TextAsset csvFile = AssetDatabase.LoadAssetAtPath<TextAsset>(csvPath);
        if (csvFile == null)
        {
            Debug.LogError($"Could not find CSV at {csvPath}");
            puzzles = new PuzzleData[0];
            return;
        }

        List<PuzzleData> loadedPuzzles = new List<PuzzleData>();
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // skip header row
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] columns = line.Split(',');
            if (columns.Length < 4) continue;

            PuzzleData data = new PuzzleData();
            data.category = "Couplets";
            data.levelNumber = int.Parse(columns[0]);
            data.answerWords = columns[1].Split(' ');
            data.jumbleLetters = columns[2];
            data.imageFileName = columns[3];

            if (!spritesByFileName.ContainsKey(data.imageFileName))
            {
                Debug.LogWarning($"Level {data.levelNumber}: no sprite found for '{data.imageFileName}'");
            }

            loadedPuzzles.Add(data);
        }

        puzzles = loadedPuzzles.ToArray();
        Debug.Log($"Loaded {puzzles.Length} puzzles from CSV.");
#else
        Debug.LogWarning("This prototype loader only works in the Unity Editor, not in a build.");
        puzzles = new PuzzleData[0];
#endif
    }

    public Sprite GetSpriteForPuzzle(PuzzleData puzzle)
    {
        spritesByFileName.TryGetValue(puzzle.imageFileName, out Sprite sprite);
        return sprite;
    }
}
#endregion