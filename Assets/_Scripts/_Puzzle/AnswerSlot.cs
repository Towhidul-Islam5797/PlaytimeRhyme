#region Summary
/// <summary>
/// This class represents an answer slot in the puzzle game, which can display different states based on the player's input. It manages the visual representation of the slot, 
/// including empty, active, correct, and wrong states. The class also keeps track of the current letter placed in the slot and whether the slot is filled or not.
/// Note: This class is designed to be used in conjunction with Unity's UI system, specifically with GameObjects and TextMeshPro for displaying text.
#endregion

#region Phase 1 Sprint 5 - AnswerSlot (4-state slot display)
using UnityEngine;
using TMPro;

public class AnswerSlot : MonoBehaviour
{
    #region Visual References
    [SerializeField] GameObject slotEmpty;
    [SerializeField] GameObject slotActive;
    [SerializeField] GameObject slotCorrect;
    [SerializeField] GameObject slotWrong;

    [SerializeField] TMP_Text activeText;
    [SerializeField] TMP_Text correctText;
    [SerializeField] TMP_Text wrongText;
    #endregion

    #region State
    char currentLetter;
    public bool IsFilled { get; private set; }
    #endregion

    #region Unity Lifecycle
    void Awake()
    {
        ShowEmpty();
    }
    #endregion

    #region Public Methods
    public void ShowEmpty()
    {
        slotEmpty.SetActive(true);
        slotActive.SetActive(false);
        slotCorrect.SetActive(false);
        slotWrong.SetActive(false);
        IsFilled = false;
        currentLetter = '\0';
    }

    public void PlaceLetter(char letter)
    {
        currentLetter = letter;
        IsFilled = true;

        slotEmpty.SetActive(false);
        slotActive.SetActive(true);
        slotCorrect.SetActive(false);
        slotWrong.SetActive(false);

        activeText.text = letter.ToString();
    }

    public void ShowCorrect()
    {
        slotEmpty.SetActive(false);
        slotActive.SetActive(false);
        slotCorrect.SetActive(true);
        slotWrong.SetActive(false);

        correctText.text = currentLetter.ToString();
    }

    public void ShowWrong()
    {
        slotEmpty.SetActive(false);
        slotActive.SetActive(false);
        slotCorrect.SetActive(false);
        slotWrong.SetActive(true);

        wrongText.text = currentLetter.ToString();
    }

    public char GetLetter()
    {
        return currentLetter;
    }
    #endregion
}
#endregion