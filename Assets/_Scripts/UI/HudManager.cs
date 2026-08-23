#region Phase 1 Sprint 7 - HUDManager (Hint/Clear/Undo/Scramble buttons)
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    #region Configuration
    [SerializeField] GameSession gameSession;
    [SerializeField] Button hintButton;
    [SerializeField] Button clearButton;
    [SerializeField] Button undoButton;
    [SerializeField] Button scrambleButton;
    #endregion

    #region Unity Lifecycle
    void Start()
    {
        hintButton.onClick.AddListener(OnHintClicked);
        clearButton.onClick.AddListener(OnClearClicked);
        undoButton.onClick.AddListener(OnUndoClicked);
        scrambleButton.onClick.AddListener(OnScrambleClicked);
    }
    #endregion

    #region Button Handlers
    public void OnHintClicked()
    {
        gameSession.GiveHint();
    }

    public void OnClearClicked()
    {
        gameSession.ClearAnswer();
    }

    public void OnUndoClicked()
    {
        gameSession.UndoLastPlacement();
    }

    public void OnScrambleClicked()
    {
        gameSession.ScrambleRemaining();
    }
    #endregion
}
#endregion