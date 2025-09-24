using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TutorialManager : MonoBehaviour
{
    public async Task Initialize()
    {
        Debug.Log("Tutorial Manager Initialized");

        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        await Task.CompletedTask;
    }
}
