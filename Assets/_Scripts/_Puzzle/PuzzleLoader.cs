#region Summary
/// This class is responsible for loading puzzle data from a CSV file and associated sprite images from a specified folder in the Unity Editor.
/// It reads the CSV file to create an array of PuzzleData objects and maps image file names to their corresponding Sprite assets.
/// Usage:
/// 1. Attach this script to a GameObject in your Unity scene.
/// 2. Set the 'folderPath' to the folder containing your sprite images and 'csvPath' to the CSV file containing puzzle data in the Unity Editor.
/// 3. The puzzles array will be populated with PuzzleData objects, and you can retrieve the corresponding Sprite for each puzzle using GetSpriteForPuzzle().
/// Note: This class currently only works in the Unity Editor and does not support loading assets in a build. Consider implementing Resources.Load or Addressables for build support if needed.
#endregion

#region Phase 1 Sprint 5 - PuzzleLoader (CSV + Sprite loading)
//using UnityEngine;
//using System.Collections.Generic;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//public class PuzzleLoader : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] string folderPath = "Assets/_Import/Level_Easy/Rhyme_Couplets_Saga";
//    [SerializeField] string csvPath = "Assets/_Import/Level_Easy/Rhyme_Couplets_Saga/Data/Couplets.csv";
//    #endregion

//    #region Data
//    public PuzzleData[] puzzles;
//    Dictionary<string, Sprite> spritesByFileName = new Dictionary<string, Sprite>();
//    #endregion

//    #region Unity Lifecycle
//    void Awake()
//    {
//#if UNITY_EDITOR
//        LoadSprites();
//        LoadPuzzlesFromCsv();
//#else
//        Debug.LogWarning("PuzzleLoader currently only works in the Unity Editor, not in a build. Real build support (Resources.Load) still needs to be decided.");
//        puzzles = new PuzzleData[0];
//#endif
//    }
//    #endregion

//    #region Loading
//#if UNITY_EDITOR
//    void LoadSprites()
//    {
//        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
//        foreach (string guid in guids)
//        {
//            string path = AssetDatabase.GUIDToAssetPath(guid);
//            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
//            string fileNameWithExtension = System.IO.Path.GetFileName(path);
//            spritesByFileName[fileNameWithExtension] = sprite;
//        }
//    }

//    void LoadPuzzlesFromCsv()
//    {
//        TextAsset csvFile = AssetDatabase.LoadAssetAtPath<TextAsset>(csvPath);
//        if (csvFile == null)
//        {
//            Debug.LogError($"Could not find CSV at {csvPath}");
//            puzzles = new PuzzleData[0];
//            return;
//        }

//        List<PuzzleData> loadedPuzzles = new List<PuzzleData>();
//        string[] lines = csvFile.text.Split('\n');

//        for (int i = 1; i < lines.Length; i++) // skip header row
//        {
//            string line = lines[i].Trim();
//            if (string.IsNullOrEmpty(line)) continue;

//            string[] columns = line.Split(',');
//            if (columns.Length < 4) continue;

//            PuzzleData data = new PuzzleData();
//            data.category = "Couplets";
//            data.levelNumber = int.Parse(columns[0]);
//            data.answerWords = columns[1].Split(' ');
//            data.jumbleLetters = columns[2];
//            data.imageFileName = columns[3];

//            if (!spritesByFileName.ContainsKey(data.imageFileName))
//            {
//                Debug.LogWarning($"Level {data.levelNumber}: no sprite found for '{data.imageFileName}'");
//            }

//            loadedPuzzles.Add(data);
//        }

//        puzzles = loadedPuzzles.ToArray();
//        Debug.Log($"Loaded {puzzles.Length} puzzles from CSV.");
//    }
//#endif
//    #endregion

//    #region Public Methods
//    public Sprite GetSpriteForPuzzle(PuzzleData puzzle)
//    {
//        spritesByFileName.TryGetValue(puzzle.imageFileName, out Sprite sprite);
//        return sprite;
//    }
//    #endregion
//}
#endregion

#region Phase 1 Sprint 6 - PuzzleLoader (Resources.Load for build support)
//using UnityEngine;
//using System.Collections.Generic;

//public class PuzzleLoader : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] string category = "Couplets";
//    public string Category => category;
//    [SerializeField] string folderPath = "Levels/Rhyme_Couplets_Saga";
//    [SerializeField] string csvPath = "Levels/Rhyme_Couplets_Saga/Data/Couplets";
//    #endregion

//    #region Data
//    public PuzzleData[] puzzles;
//    Dictionary<string, Sprite> spritesByFileName = new Dictionary<string, Sprite>();
//    #endregion

//    #region Unity Lifecycle
//    void Awake()
//    {
//        LoadSprites();
//        LoadPuzzlesFromCsv();
//    }
//    #endregion

//    #region Loading
//    void LoadSprites()
//    {
//        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);
//        foreach (Sprite sprite in sprites)
//        {
//            spritesByFileName[sprite.name] = sprite;
//        }
//    }

//    void LoadPuzzlesFromCsv()
//    {
//        TextAsset csvFile = Resources.Load<TextAsset>(csvPath);
//        if (csvFile == null)
//        {
//            Debug.LogError($"Could not find CSV at Resources/{csvPath}");
//            puzzles = new PuzzleData[0];
//            return;
//        }

//        List<PuzzleData> loadedPuzzles = new List<PuzzleData>();
//        string[] lines = csvFile.text.Split('\n');

//        for (int i = 1; i < lines.Length; i++) // skip header row
//        {
//            string line = lines[i].Trim();
//            if (string.IsNullOrEmpty(line)) continue;

//            string[] columns = line.Split(',');
//            if (columns.Length < 4) continue;

//            PuzzleData data = new PuzzleData();
//            data.category = category;
//            data.levelNumber = int.Parse(columns[0]);
//            data.answerWords = columns[1].Split(' ');
//            data.jumbleLetters = columns[2];
//            data.imageFileName = columns[3];

//            string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(data.imageFileName);
//            if (!spritesByFileName.ContainsKey(nameWithoutExtension))
//            {
//                Debug.LogWarning($"Level {data.levelNumber}: no sprite found for '{data.imageFileName}'");
//            }

//            loadedPuzzles.Add(data);
//        }

//        puzzles = loadedPuzzles.ToArray();
//        Debug.Log($"Loaded {puzzles.Length} puzzles from CSV.");
//    }
//    #endregion

//    #region Public Methods
//    public Sprite GetSpriteForPuzzle(PuzzleData puzzle)
//    {
//        string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(puzzle.imageFileName);
//        spritesByFileName.TryGetValue(nameWithoutExtension, out Sprite sprite);
//        return sprite;
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - PuzzleLoader (Resources.Load for build support, runtime category switching)
//using UnityEngine;
//using System.Collections.Generic;

//public class PuzzleLoader : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] string category = "Couplets";
//    public string Category => category;
//    [SerializeField] string folderPath = "Levels/Rhyme_Couplets_Saga";
//    [SerializeField] string csvPath = "Levels/Rhyme_Couplets_Saga/Data/Couplets";
//    #endregion

//    #region Data
//    public PuzzleData[] puzzles;
//    Dictionary<string, Sprite> spritesByFileName = new Dictionary<string, Sprite>();
//    #endregion

//    #region Unity Lifecycle
//    void Awake()
//    {
//        LoadSprites();
//        LoadPuzzlesFromCsv();
//    }
//    #endregion

//    #region Loading
//    void LoadSprites()
//    {
//        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);
//        foreach (Sprite sprite in sprites)
//        {
//            spritesByFileName[sprite.name] = sprite;
//        }
//    }

//    void LoadPuzzlesFromCsv()
//    {
//        TextAsset csvFile = Resources.Load<TextAsset>(csvPath);
//        if (csvFile == null)
//        {
//            Debug.LogError($"Could not find CSV at Resources/{csvPath}");
//            puzzles = new PuzzleData[0];
//            return;
//        }

//        List<PuzzleData> loadedPuzzles = new List<PuzzleData>();
//        string[] lines = csvFile.text.Split('\n');

//        for (int i = 1; i < lines.Length; i++) // skip header row
//        {
//            string line = lines[i].Trim();
//            if (string.IsNullOrEmpty(line)) continue;

//            string[] columns = line.Split(',');
//            if (columns.Length < 4) continue;

//            PuzzleData data = new PuzzleData();
//            data.category = category;
//            data.levelNumber = int.Parse(columns[0]);
//            data.answerWords = columns[1].Split(' ');
//            data.jumbleLetters = columns[2];
//            data.imageFileName = columns[3];

//            string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(data.imageFileName);
//            if (!spritesByFileName.ContainsKey(nameWithoutExtension))
//            {
//                Debug.LogWarning($"Level {data.levelNumber}: no sprite found for '{data.imageFileName}'");
//            }

//            loadedPuzzles.Add(data);
//        }

//        puzzles = loadedPuzzles.ToArray();
//        Debug.Log($"Loaded {puzzles.Length} puzzles from CSV.");
//    }
//    #endregion

//    #region Public Methods
//    public Sprite GetSpriteForPuzzle(PuzzleData puzzle)
//    {
//        string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(puzzle.imageFileName);
//        spritesByFileName.TryGetValue(nameWithoutExtension, out Sprite sprite);
//        return sprite;
//    }

//    public void LoadCategory(string newCategory, string newFolderPath, string newCsvPath)
//    {
//        category = newCategory;
//        folderPath = newFolderPath;
//        csvPath = newCsvPath;

//        spritesByFileName.Clear();
//        LoadSprites();
//        LoadPuzzlesFromCsv();
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - PuzzleLoader (Resources.Load for build support, runtime category switching)
//using UnityEngine;
//using System.Collections.Generic;

//public class PuzzleLoader : MonoBehaviour
//{
//    #region Data
//    string category;
//    public string Category => category;
//    public PuzzleData[] puzzles = new PuzzleData[0];
//    Dictionary<string, Sprite> spritesByFileName = new Dictionary<string, Sprite>();
//    #endregion

//    #region Loading
//    void LoadSprites(string folderPath)
//    {
//        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);
//        foreach (Sprite sprite in sprites)
//        {
//            spritesByFileName[sprite.name] = sprite;
//        }
//    }

//    void LoadPuzzlesFromCsv(string csvPath)
//    {
//        TextAsset csvFile = Resources.Load<TextAsset>(csvPath);
//        if (csvFile == null)
//        {
//            Debug.LogError($"Could not find CSV at Resources/{csvPath}");
//            puzzles = new PuzzleData[0];
//            return;
//        }

//        List<PuzzleData> loadedPuzzles = new List<PuzzleData>();
//        string[] lines = csvFile.text.Split('\n');

//        for (int i = 1; i < lines.Length; i++) // skip header row
//        {
//            string line = lines[i].Trim();
//            if (string.IsNullOrEmpty(line)) continue;

//            string[] columns = line.Split(',');
//            if (columns.Length < 4) continue;

//            PuzzleData data = new PuzzleData();
//            data.category = category;
//            data.levelNumber = int.Parse(columns[0]);
//            data.answerWords = columns[1].Split(' ');
//            data.jumbleLetters = columns[2];
//            data.imageFileName = columns[3];

//            string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(data.imageFileName);
//            if (!spritesByFileName.ContainsKey(nameWithoutExtension))
//            {
//                Debug.LogWarning($"Level {data.levelNumber}: no sprite found for '{data.imageFileName}'");
//            }

//            loadedPuzzles.Add(data);
//        }

//        puzzles = loadedPuzzles.ToArray();
//        Debug.Log($"Loaded {puzzles.Length} puzzles from CSV.");
//    }
//    #endregion

//    #region Public Methods
//    public Sprite GetSpriteForPuzzle(PuzzleData puzzle)
//    {
//        string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(puzzle.imageFileName);
//        spritesByFileName.TryGetValue(nameWithoutExtension, out Sprite sprite);
//        return sprite;
//    }

//    public void LoadCategory(string newCategory, string newFolderPath, string newCsvPath)
//    {
//        category = newCategory;

//        spritesByFileName.Clear();
//        LoadSprites(newFolderPath);
//        LoadPuzzlesFromCsv(newCsvPath);
//    }
//    #endregion
//}
#endregion

#region Milestone 1 - PuzzleLoader (holds category list, loads by index)
using UnityEngine;
using System.Collections.Generic;

public class PuzzleLoader : MonoBehaviour
{
    #region Category List
    [System.Serializable]
    public class CategoryEntry
    {
        public string categoryName;
        public string folderPath;
        public string csvPath;
    }

    [SerializeField] List<CategoryEntry> categories;
    #endregion

    #region Data
    string category;
    public string Category => category;
    public PuzzleData[] puzzles = new PuzzleData[0];
    Dictionary<string, Sprite> spritesByFileName = new Dictionary<string, Sprite>();
    #endregion

    #region Loading
    void LoadSprites(string folderPath)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);
        foreach (Sprite sprite in sprites)
        {
            spritesByFileName[sprite.name] = sprite;
        }
    }

    void LoadPuzzlesFromCsv(string csvPath)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvPath);
        if (csvFile == null)
        {
            Debug.LogError($"Could not find CSV at Resources/{csvPath}");
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
            data.category = category;
            data.levelNumber = int.Parse(columns[0]);
            data.answerWords = columns[1].Split(' ');
            data.jumbleLetters = columns[2];
            data.imageFileName = columns[3];

            string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(data.imageFileName);
            if (!spritesByFileName.ContainsKey(nameWithoutExtension))
            {
                Debug.LogWarning($"Level {data.levelNumber}: no sprite found for '{data.imageFileName}'");
            }

            loadedPuzzles.Add(data);
        }

        puzzles = loadedPuzzles.ToArray();
        Debug.Log($"Loaded {puzzles.Length} puzzles from CSV.");
    }
    #endregion

    #region Public Methods
    public Sprite GetSpriteForPuzzle(PuzzleData puzzle)
    {
        string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(puzzle.imageFileName);
        spritesByFileName.TryGetValue(nameWithoutExtension, out Sprite sprite);
        return sprite;
    }

    public void LoadCategory(int categoryIndex)
    {
        CategoryEntry entry = categories[categoryIndex];
        category = entry.categoryName;

        spritesByFileName.Clear();
        LoadSprites(entry.folderPath);
        LoadPuzzlesFromCsv(entry.csvPath);
    }

    public int CategoryCount => categories.Count;
    #endregion
}
#endregion