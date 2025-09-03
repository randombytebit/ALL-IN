using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class MenuInitiator : MonoBehaviour
{
    [SerializeField] private GameObject _gameManagerPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _objectPoolManagerPrefab;
    [SerializeField] private GameObject _directionalLightPrefab;
    [SerializeField] private GameObject _textMeshProPrefab;
    [SerializeField] private List<GameObject> _menuPokerCardPrefabs;

    // References to instantiated objects
    private GameManager _gameManager;
    private PlayerSetting _player;
    private float _pokercardXOrigin = -1.25f;


    private async void Start()
    {
        _objectPoolManagerPrefab = Instantiate(_objectPoolManagerPrefab);
        await InitializeObjects();
        // _loadingScene.show();
    }

    private async Task InitializeObjects()
    {
        // Spawn and get reference to GameManager
        GameObject gameManagerObj = ObjectPoolManager.SpawnPooledObject(_gameManagerPrefab,
            Vector3.zero,
            Quaternion.identity,
            ObjectPoolManager.ObjectPoolType.ManagerObject);
        _gameManager = gameManagerObj.GetComponent<GameManager>();

        if (_gameManager != null)
        {
            await _gameManager.Initialize();
        }

        // Spawn and get reference to Player
        GameObject playerObj = ObjectPoolManager.SpawnPooledObject(_playerPrefab,
            new Vector3(-0.02f, 2.58f, -5.21f),
            Quaternion.identity,
            ObjectPoolManager.ObjectPoolType.PlayerObject);
        _player = playerObj.GetComponent<PlayerSetting>();

        if (_player != null)
        {
            await _player.Initialize();
        }

        // Set up lighting
        GameObject lightObj = ObjectPoolManager.SpawnPooledObject(_directionalLightPrefab,
            new Vector3(0.77f, 13.21f, 1.37f),
            Quaternion.Euler(60f, 0f, 0f),
            ObjectPoolManager.ObjectPoolType.ManagerObject);

        GameObject textMeshObj = ObjectPoolManager.SpawnPooledObject(_textMeshProPrefab,
            Vector3.zero,
            Quaternion.Euler(90f, 0f, 0f),
            ObjectPoolManager.ObjectPoolType.ManagerObject);
        TextMeshPro textMeshComponent = textMeshObj.GetComponent<TextMeshPro>();

        // Spawn MenuPokerCards
        foreach (GameObject cardPrefab in _menuPokerCardPrefabs)
        {
            _pokercardXOrigin += 0.25f;
            GameObject spawnedCard = ObjectPoolManager.SpawnPooledObject(cardPrefab,
                new Vector3(_pokercardXOrigin, 2.56f, -0.83f),
                Quaternion.identity,
                ObjectPoolManager.ObjectPoolType.ManagerObject);

            // Get the MenuPokerCard component and assign the TextMeshPro
            if (spawnedCard.TryGetComponent<MenuPokerCard>(out MenuPokerCard menuPokerCard))
            {
                // Set the TextMeshPro component reference on the MenuPokerCard
                menuPokerCard.SetTextMeshPro(textMeshComponent);
                
                await menuPokerCard.Initialize();
            }
        }
    }
}
