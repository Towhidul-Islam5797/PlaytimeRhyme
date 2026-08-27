#region Milestone 2 - LevelNode (level-select map node)
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
    #endregion

    #region Data
    public int levelNumber { get; private set; }
    public bool isCompleted { get; private set; }
    #endregion

    #region Setup
    public void Setup(int levelNumber, bool isLocked, bool isCompleted, int starsEarned = 0)
    {
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

    #region Tap Handling
    public void OnNodeTapped()
    {
        Debug.Log($"Level {levelNumber} tapped.");
        // Later: load GameScene with this levelNumber
    }
    #endregion
}
#endregion