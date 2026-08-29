#region Milestone 1 - LoadingScreenController (async scene transition)
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] Slider progressSlider;

    void Start()
    {
        StartCoroutine(LoadTargetScene());
    }

    System.Collections.IEnumerator LoadTargetScene()
    {
        string targetScene = PendingScene.sceneName;

        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            progressSlider.value = operation.progress;
            yield return null;
        }

        progressSlider.value = 1f;
        operation.allowSceneActivation = true;
    }
}
#endregion