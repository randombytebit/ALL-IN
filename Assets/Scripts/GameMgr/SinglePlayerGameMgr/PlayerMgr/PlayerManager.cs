using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _aiPrefab;


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
