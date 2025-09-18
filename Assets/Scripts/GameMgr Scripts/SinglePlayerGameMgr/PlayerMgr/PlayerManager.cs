using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _aiPrefab;

    public async Task Initialize()
    {
        Debug.Log("PlayerManager initialized");
        await Task.CompletedTask;
    }

    public void CreatePlayer()
    {
        ObjectPoolManager.SpawnPooledObject(
            _playerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.PlayerObject
        );
    }

    public void CreateAI()
    {
        ObjectPoolManager.SpawnPooledObject(
            _aiPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.PlayerObject
        );
    }
}
