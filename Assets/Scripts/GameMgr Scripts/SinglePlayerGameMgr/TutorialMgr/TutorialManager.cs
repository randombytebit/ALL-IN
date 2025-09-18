using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TutorialManager : MonoBehaviour
{
    public async Task Initialize()
    {
        Debug.Log("Tutorial Manager Initialized");
        await Task.CompletedTask;
    }
}
