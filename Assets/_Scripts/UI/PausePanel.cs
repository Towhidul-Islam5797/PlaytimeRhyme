#region Phase 1 Sprint 7 - PausePanel (pause menu controls)
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    #region Configuration
    [Header("References")]
    [SerializeField] GameSession gameSession;
    [SerializeField] GameObject pausePanelRoot;
    #endregion

    #region State
    bool isSoundOn = true;
    bool isMusicOn = true;
    #endregion

    #region Open / Close
    public void OpenPause()
    {
        pausePanelRoot.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePause()
    {
        pausePanelRoot.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion

    #region Button Handlers
    public void OnResumeClicked()
    {
        ClosePause();
    }

    public void OnRestartClicked()
    {
        gameSession.ClearAnswer();
        ClosePause();
    }

    public void OnSoundToggleClicked()
    {
        isSoundOn = !isSoundOn;
    }

    public void OnMusicToggleClicked()
    {
        isMusicOn = !isMusicOn;
    }
    #endregion
}
#endregion