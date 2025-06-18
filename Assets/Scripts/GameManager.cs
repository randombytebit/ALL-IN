using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 160;
        Cursor.visible = false;

        // Releases the cursor
        // Cursor.lockState = CursorLockMode.None;

        // Locks the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Confines the cursor
        // Cursor.lockState = CursorLockMode.Confined;
    }
}
