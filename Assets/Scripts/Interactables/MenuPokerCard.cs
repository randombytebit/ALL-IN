using TMPro;
using UnityEngine;

public enum MenuState
{
    Null,
    GameModeSelection,
    Appearance,
    Leaderboard,
    Settings,
    Quit
}

public enum GameMode
{
    Null,
    Casual,
    Ranked,
    PrivateLobby,
    Tutorial
}

public class MenuPokerCard : Interactable
{
    [Header("Poker Card Configuration")]
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private string pokerCardMessage;

    [Header("Game State Configuration")]
    [SerializeField] private MenuState targetMenuState;
    [SerializeField] private GameMode targetGameMode;

    // public getters for external access
    public MenuState TargetMenuState => targetMenuState;
    public GameMode TargetGameMode => targetGameMode;

    protected override void Interact()
    {
        Debug.Log("Interacted with PokerCard: " + pokerCardMessage);

        if (targetMenuState != MenuState.Null)
        {
            GameManager.Instance.SetMenuState(targetMenuState);
        }
        else if (targetGameMode != GameMode.Null)
        {
            GameManager.Instance.SetGameMode(targetGameMode);
            textMeshPro.text = "";
        }
    }

    protected override void Point()
    {
        textMeshPro.text = pokerCardMessage;

        Vector3 cardPosition = transform.position;
        textMeshPro.transform.position = cardPosition + new Vector3(0.75f, 0, 0.5f);
        Debug.Log(textMeshPro.transform.position);
    }
}
