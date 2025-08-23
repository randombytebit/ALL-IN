using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerManagerPrefab;
    [SerializeField] private GameObject _DeckManagerPrefab;
    [SerializeField] private GameObject _PokerLogicManagerPrefab;
    [SerializeField] private GameObject _TurnManagerPrefab;
    [SerializeField] private GameObject _BettingManagerPrefab;
    [SerializeField] private GameObject _RoundManagerPrefab;
    [SerializeField] private GameObject _TutorialManagerPrefab;

    public void InitializeManagers()
    {
        // Initialize all single player mode managers
        ObjectPoolManager.SpawnPooledObject(_playerManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        ObjectPoolManager.SpawnPooledObject(_DeckManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        ObjectPoolManager.SpawnPooledObject(_PokerLogicManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        ObjectPoolManager.SpawnPooledObject(_TurnManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        ObjectPoolManager.SpawnPooledObject(_BettingManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        ObjectPoolManager.SpawnPooledObject(_RoundManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        ObjectPoolManager.SpawnPooledObject(_TutorialManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
    }
}
