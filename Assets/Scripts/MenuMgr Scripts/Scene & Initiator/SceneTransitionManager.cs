using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    private string currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    void Start()
    {
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        yield return loadOperation;
        Scene newScene = SceneManager.GetSceneByName("MenuScene");
        SceneManager.SetActiveScene(newScene);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
