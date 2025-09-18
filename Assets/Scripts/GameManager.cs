using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<MenuPokerCard> pokerCards;

    [Header("Current Game State")]
    public MenuState menuState;
    private GameMode selectedGameMode;
    private string currentScene;

    public System.Action<MenuState> OnMenuStateChanged;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task Initialize()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pokerCards = new List<MenuPokerCard>(FindObjectsOfType<MenuPokerCard>());

        foreach (MenuPokerCard card in pokerCards)
        {
            if (card.TargetMenuState == MenuState.Null)
            {
                card.gameObject.active = false;
            }
            else
            {
                card.gameObject.active = true;
            }
        }

        await Task.CompletedTask;
    }

    void Start()
    {
    }

    public void SetMenuState(MenuState newState)
    {
        if (menuState != newState)
        {
            menuState = newState;
            OnMenuStateChanged?.Invoke(newState);
            Debug.Log("Menu state changed to: " + newState);
        }
        if (newState == MenuState.GameModeSelection)
        {
            UpdateCardVisibility(false);
        }
        else
        {
            UpdateCardVisibility(true);
        }
    }

    public void SetGameMode(GameMode gameMode)
    {
        selectedGameMode = gameMode;
        Debug.Log("Game mode selected: " + gameMode);
        StartGame();
    }

    private void UpdateCardVisibility(bool showMenuState)
    {
        foreach (MenuPokerCard card in pokerCards)
        {
            if (showMenuState)
            {
                card.gameObject.SetActive(card.TargetMenuState != MenuState.Null);
            }
            else
            {    
                card.gameObject.SetActive(card.TargetGameMode != GameMode.Null);
            }
        }
    }

    private void DisvisbleAllCards()
    {
        foreach (MenuPokerCard card in pokerCards)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        Debug.Log("Starting game with mode: " + selectedGameMode);
        DisvisbleAllCards();
        switch (selectedGameMode)
        {
            case GameMode.Casual:
                break;
            case GameMode.Ranked:
                break;
            case GameMode.PrivateLobby:
                break;
            case GameMode.Tutorial:
                StartCoroutine(LoadNewScene("TutorialScene"));
                break;
        }
    }

    private IEnumerator LoadNewScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        
        // Clear existing pooled objects
        ObjectPoolManager.ClearAllPools();
        
        // Load new scene
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Update current scene reference
        currentScene = sceneName;
        
        Debug.Log($"Scene {sceneName} loaded successfully");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
