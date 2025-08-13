using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<MenuPokerCard> pokerCards;

    [Header("Current Game State")]
    public MenuState menuState;
    private GameMode selectedGameMode;

    public System.Action<MenuState> OnMenuStateChanged;

    void Awake()
    {
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

    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pokerCards = new List<MenuPokerCard>(FindObjectsOfType<MenuPokerCard>(true));

        // Initialize GameModeCards as inactive
        foreach (MenuPokerCard card in pokerCards)
        {
            if (card.TargetGameMode != GameMode.Null)
            {
                card.gameObject.SetActive(false);
            }
        }
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
                CasualGameModeController.Instance.StartGame();
                break;
            case GameMode.Ranked:
                RankedGameModeController.Instance.StartGame();
                break;
            case GameMode.PrivateLobby:
                PrivateLobbyGameModeController.Instance.StartGame();
                break;
            case GameMode.Tutorial:
                TutorialGameModeController.Instance.StartGame();
                break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
