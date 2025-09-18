using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PokerLogicManager : MonoBehaviour
{
    public async Task Initialize()
    {
        Debug.Log("PokerLogicManager initialized.");
        await Task.CompletedTask;
    }
}
