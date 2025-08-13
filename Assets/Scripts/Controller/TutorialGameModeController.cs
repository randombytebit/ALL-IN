using UnityEngine;

public class TutorialGameModeController : MonoBehaviour
{
    public static TutorialGameModeController Instance { get; private set; }

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
        Debug.Log("Starting Tutorial Game Mode");
    }
}