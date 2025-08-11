using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    StartGame,
    Appearance,
    Leaderboard,
    Settings,
    Quit
}

public enum GameMode
{
    Causal,
    Ranked,
    PrivateLobby,
    Tutorial
}

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 160;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
