using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RoundManager : MonoBehaviour
{
    public async Task Initialize()
    {
        Debug.Log("RoundManager initialized.");
        await Task.CompletedTask;
    }
}   
