#region Phase 1 Sprint 7 - SceneShuffler (scene loading)
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShuffler : MonoBehaviour
{
    public void LoadingScene()
    {
        SceneManager.LoadScene("Loading");
    }

    public void LoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}
#endregion