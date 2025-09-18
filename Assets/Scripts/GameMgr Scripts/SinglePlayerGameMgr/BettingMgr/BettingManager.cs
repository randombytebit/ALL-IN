using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BettingManager : MonoBehaviour
{
    public async Task Initialize()
    {
        Debug.Log("BettingManager initialized.");
        await Task.CompletedTask;
    }
}
