#region Summary 
/// <summary>
/// This class represents a puzzle tile in the game, which displays a letter and can be tapped by the player. It manages the visual representation of the tile, 
///     including displaying the letter and handling tap events. The class also keeps track of the letter associated with the tile and communicates with the GameSession to handle game logic when the tile is tapped.
/// Note: This class is designed to be used in conjunction with Unity's UI system, specifically with GameObjects, Buttons, and TextMeshPro for displaying text.

#endregion
#region Phase 1 Sprint 5 - PuzzleTile (tappable letter tile)
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleTile : MonoBehaviour
{
    #region Visual References
    [SerializeField] TMP_Text letterText;
    [SerializeField] Button button;
    #endregion

    #region State
    public char Letter { get; private set; }
    GameSession gameSession;
    #endregion

    #region Setup
    public void Setup(char letter, GameSession session)
    {
        Letter = letter;
        gameSession = session;
        letterText.text = letter.ToString();

        button.onClick.AddListener(OnTapped);
    }
    #endregion

    #region Tap Handling
    void OnTapped()
    {
        gameSession.OnTileTapped(this);
    }
    #endregion
}
#endregion