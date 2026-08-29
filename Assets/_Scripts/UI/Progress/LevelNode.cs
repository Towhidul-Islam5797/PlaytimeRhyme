#region Milestone 1 - LevelNode (level-select map node)
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class LevelNode : MonoBehaviour
//{
//    #region Configuration
//    [SerializeField] GameObject buttonProgress;
//    [SerializeField] Button buttonProgressButton;
//    [SerializeField] TMP_Text progressLevelNumberText;

//    [SerializeField] GameObject buttonCompleted;
//    [SerializeField] TMP_Text completedLevelNumberText;
//    [SerializeField] GameObject[] starFilledObjects;
//    [SerializeField] GameObject[] starEmptyObjects;
//    #endregion

//    #region Data
//    public int levelNumber { get; private set; }
//    public bool isCompleted { get; private set; }
//    #endregion

//    #region Setup
//    public void Setup(int levelNumber, bool isLocked, bool isCompleted, int starsEarned = 0)
//    {
//        this.levelNumber = levelNumber;
//        this.isCompleted = isCompleted;

//        buttonProgress.SetActive(!isCompleted);
//        buttonCompleted.SetActive(isCompleted);

//        if (isCompleted)
//        {
//            completedLevelNumberText.text = levelNumber.ToString();
//            SetStars(starsEarned);
//        }
//        else
//        {
//            progressLevelNumberText.text = levelNumber.ToString();
//            buttonProgressButton.interactable = !isLocked;
//        }
//    }

//    void SetStars(int starsEarned)
//    {
//        for (int i = 0; i < starFilledObjects.Length; i++)
//        {
//            bool filled = i < starsEarned;
//            starFilledObjects[i].SetActive(filled);
//            starEmptyObjects[i].SetActive(!filled);
//        }
//    }
//    #endregion

//    #region Tap Handling
//    public void OnNodeTapped()
//    {
//        Debug.Log($"Level {levelNumber} tapped.");
//        // Later: load GameScene with this levelNumber
//    }
//    #endregion
//}
#endregion

// ============================================================
// SETUP INSTRUCTIONS
// ============================================================
// File: Assets/_Scripts/UI/Progress/LevelNode.cs
// Action: REPLACE the entire file with this content.
//
// WHAT CHANGED FROM YOUR CURRENT VERSION:
// - Setup() now takes a categoryIndex parameter (in addition to
//   the existing ones) - the node needs to know which category
//   it belongs to, not just its level number.
// - New field: sceneShuffler - a reference to the SceneShuffler
//   component, used to actually load GameScene when tapped.
// - OnNodeTapped() now stores the tapped level into SelectedLevel
//   and loads GameScene, instead of just logging.
//
// AFTER PASTING - IN UNITY, ON THE LevelNodePrefab:
// 1. Select the LevelNodePrefab asset (in _Prefabs/) to edit it
// 2. On the LevelNode (Script) component, a new field appears:
//    "Scene Shuffler"
// 3. Drag a SceneShuffler component into this field. Since
//    LevelNodePrefab instances live inside MapSegment prefabs
//    which get instantiated into MapScene, and MapScene already
//    has a SceneShuffler on its "SceneShuffler" GameObject, drag
//    THAT into this field on the prefab.
//    NOTE: if this causes issues (since the prefab is edited in
//    isolation and can't see the scene's SceneShuffler), the
//    more robust option is to have LevelNode find it at runtime
//    instead - see the commented-out alternative Awake() method
//    below the Setup region, ready to swap in if needed.
// 4. This needs to be done once - since all node instances came
//    from the same prefab, this fix applies everywhere at once.
// ============================================================

#region Milestone 1 - LevelNode (level-select map node, tap-to-play)
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelNode : MonoBehaviour
{
    #region Configuration
    [SerializeField] GameObject buttonProgress;
    [SerializeField] Button buttonProgressButton;
    [SerializeField] TMP_Text progressLevelNumberText;

    [SerializeField] GameObject buttonCompleted;
    [SerializeField] TMP_Text completedLevelNumberText;
    [SerializeField] GameObject[] starFilledObjects;
    [SerializeField] GameObject[] starEmptyObjects;

    //[SerializeField] SceneShuffler sceneShuffler;
    #endregion

    #region Data
    public int categoryIndex { get; private set; }
    public int levelNumber { get; private set; }
    public bool isCompleted { get; private set; }
    #endregion

    #region Setup
    public void Setup(int categoryIndex, int levelNumber, bool isLocked, bool isCompleted, int starsEarned = 0)
    {
        this.categoryIndex = categoryIndex;
        this.levelNumber = levelNumber;
        this.isCompleted = isCompleted;

        buttonProgress.SetActive(!isCompleted);
        buttonCompleted.SetActive(isCompleted);

        if (isCompleted)
        {
            completedLevelNumberText.text = levelNumber.ToString();
            SetStars(starsEarned);
        }
        else
        {
            progressLevelNumberText.text = levelNumber.ToString();
            buttonProgressButton.interactable = !isLocked;
        }
    }

    void SetStars(int starsEarned)
    {
        for (int i = 0; i < starFilledObjects.Length; i++)
        {
            bool filled = i < starsEarned;
            starFilledObjects[i].SetActive(filled);
            starEmptyObjects[i].SetActive(!filled);
        }
    }
    #endregion

    // ----------------------------------------------------------
    // ALTERNATIVE: if dragging SceneShuffler onto the prefab
    // directly doesn't work well (prefab can't reference a scene
    // object), delete the [SerializeField] SceneShuffler field
    // above and use this instead - it finds SceneShuffler in the
    // scene automatically at runtime, no manual wiring needed:
    //
    SceneShuffler sceneShuffler;
    void Awake()
    {
        sceneShuffler = FindObjectOfType<SceneShuffler>();
    }
    // ----------------------------------------------------------

    #region Tap Handling
    public void OnNodeTapped()
    {
        SelectedLevel.categoryIndex = categoryIndex;
        SelectedLevel.levelNumber = levelNumber;
        sceneShuffler.GameScene();
    }
    #endregion
}
#endregion