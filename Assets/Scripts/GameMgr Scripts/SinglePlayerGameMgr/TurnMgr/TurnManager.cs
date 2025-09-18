using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TurnManager : MonoBehaviour
{
    public async Task Initialize()
    {
        Debug.Log("TurnManager initialized.");
        await Task.CompletedTask;
    }
}
