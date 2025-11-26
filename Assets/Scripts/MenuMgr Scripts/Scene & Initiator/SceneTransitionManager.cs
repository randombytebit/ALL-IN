using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private string _targetSceneName = "MenuScene";
    [SerializeField] private bool _unloadCurrentScene = true;
    [SerializeField] private bool _setAsActiveScene = true;

    private void Start()
    {
        _ = TransitionToSceneAsync(); // Fire and forget
    }

    private async Task TransitionToSceneAsync()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log($"[SceneTransition] Loading '{_targetSceneName}' from '{currentScene}'");

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(_targetSceneName, LoadSceneMode.Additive);
        if (loadOp == null)
        {
            Debug.LogError($"[SceneTransition] Failed to load scene: {_targetSceneName}");
            return;
        }


        while (!loadOp.isDone)
            await Task.Yield();


        Scene targetScene = SceneManager.GetSceneByName(_targetSceneName);
        if (!targetScene.IsValid())
        {
            Debug.LogError($"[SceneTransition] Loaded scene not valid: {_targetSceneName}");
            return;
        }

        if (_setAsActiveScene)
        {
            SceneManager.SetActiveScene(targetScene);
            Debug.Log($"[SceneTransition] Active scene: {_targetSceneName}");
        }

        if (_unloadCurrentScene)
        {
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(currentScene);
            if (unloadOp != null)
            {
                while (!unloadOp.isDone)
                    await Task.Yield();
                Debug.Log($"[SceneTransition] Unloaded: {currentScene}");
            }
        }
    }
}