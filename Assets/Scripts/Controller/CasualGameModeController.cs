using UnityEngine;

public class CasualGameModeController : MonoBehaviour
{
    public static CasualGameModeController Instance { get; private set; }

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
        Debug.Log("Starting Casual Game Mode");
    }
}