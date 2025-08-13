using UnityEngine;

public class RankedGameModeController : MonoBehaviour
{
    public static RankedGameModeController Instance { get; private set; }

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
        Debug.Log("Starting Ranked Game Mode");
    }
}