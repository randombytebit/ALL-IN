using UnityEngine;

public class PrivateLobbyGameModeController : MonoBehaviour
{
    public static PrivateLobbyGameModeController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Debug.Log("Starting PrivateLobby Game Mode");
    }
}