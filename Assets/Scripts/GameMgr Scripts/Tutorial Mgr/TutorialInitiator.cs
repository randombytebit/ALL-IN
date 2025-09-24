using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class TutorialInitiator : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _objectPoolManagerPrefab;
    [SerializeField] private GameObject _directionalLightPrefab;
    [SerializeField] private GameObject _playerManagerPrefab;
    [SerializeField] private GameObject _DeckManagerPrefab;
    [SerializeField] private GameObject _PokerLogicManagerPrefab;
    [SerializeField] private GameObject _TurnManagerPrefab;
    [SerializeField] private GameObject _BettingManagerPrefab;
    [SerializeField] private GameObject _RoundManagerPrefab;
    [SerializeField] private GameObject _TutorialManagerPrefab;
    [SerializeField] private GameObject _settingsCanvas;
    private GameObject _objectPoolManager;
    private PlayerSetting _player;
    private PlayerManager _playerManager;
    private DeckManager _deckManager;
    private PokerLogicManager _pokerLogicManager;
    private TurnManager _turnManager;
    private BettingManager _bettingManager;
    private RoundManager _roundManager;
    private TutorialManager _tutorialManager;
    private bool _hasInitialized = false;

    private void OnEnable()
    {
        Debug.Log("TutorialInitiator OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Call when the game is terminated
    private void OnDisable()
    {
        Debug.Log("TutorialInitiator OnDisable called");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        Debug.Log("TutorialInitiator Start called");
        // StartCoroutine(DelayedStart());
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_settingsCanvas != null)
            {
                bool isActive = _settingsCanvas.activeSelf;
                _settingsCanvas.SetActive(!isActive);
            }

            // Cursor visibility and lock state
            Cursor.visible = _settingsCanvas.activeSelf;
            Cursor.lockState = _settingsCanvas.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;

            // Freeze the timer
            Time.timeScale = _settingsCanvas.activeSelf ? 0f : 1f;
        }
    }

    private IEnumerator DelayedStart()
    {
        // Wait for two frames to ensure scene is fully loaded
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        if (!_hasInitialized)
        {
            _hasInitialized = true;
            yield return StartCoroutine(InitializeObjectsCoroutine());
            Debug.Log("Tutorial initialization completed");
        }
    }

    // Add this new coroutine method
    private IEnumerator InitializeObjectsCoroutine()
    {
        var task = InitializeObjects();
        while (!task.IsCompleted)
        {
            yield return null;
        }
        
        if (task.Exception != null)
        {
            Debug.LogError($"Initialization failed: {task.Exception}");
        }
    }

    private async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded: {scene.name} with mode: {mode}");

        if (_hasInitialized)
        {
            Debug.Log("Objects already initialized, skipping initialization");
            return;
        }

        // Check if ObjectPoolManager already exists
        if (ObjectPoolManager.Instance == null)
        {
            Debug.Log("Creating new ObjectPoolManager instance");
            _objectPoolManager = Instantiate(_objectPoolManagerPrefab);
            DontDestroyOnLoad(_objectPoolManager);
            await Task.Yield();
        }
        else
        {
            Debug.Log("ObjectPoolManager instance already exists");
        }
        
        Debug.Log("Starting object initialization");
        await InitializeObjects();
        Debug.Log("Object initialization completed");
    }

    private async Task InitializeObjects()
    {
        await Task.Yield();
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

        // Spawn _playerManagerPrefab
        GameObject playerManagerObj = ObjectPoolManager.SpawnPooledObject(_playerManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        _playerManager = playerManagerObj.GetComponent<PlayerManager>();
        if (_playerManager != null)
        {
            await _playerManager.Initialize();
        }

        // Spawn _DeckManagerPrefab
        GameObject deckManagerObj = ObjectPoolManager.SpawnPooledObject(_DeckManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        _deckManager = deckManagerObj.GetComponent<DeckManager>();
        if (_deckManager != null)
        {
            await _deckManager.Initialize();
        }

        // Spawn _PokerLogicManagerPrefab
        GameObject pokerLogicManagerObj = ObjectPoolManager.SpawnPooledObject(_PokerLogicManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        _pokerLogicManager = pokerLogicManagerObj.GetComponent<PokerLogicManager>();
        if (_pokerLogicManager != null)
        {
            await _pokerLogicManager.Initialize();
        }

        // Spawn _TurnManagerPrefab
        GameObject turnManagerObj = ObjectPoolManager.SpawnPooledObject(_TurnManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        _turnManager = turnManagerObj.GetComponent<TurnManager>();
        if (_turnManager != null)
        {
            await _turnManager.Initialize();
        }

        // Spawn _BettingManagerPrefab
        GameObject bettingManagerObj = ObjectPoolManager.SpawnPooledObject(_BettingManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        _bettingManager = bettingManagerObj.GetComponent<BettingManager>();
        if (_bettingManager != null)
        {
            await _bettingManager.Initialize();
        }

        // Spawn _RoundManagerPrefab
        GameObject roundManagerObj = ObjectPoolManager.SpawnPooledObject(_RoundManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        _roundManager = roundManagerObj.GetComponent<RoundManager>();
        if (_roundManager != null)
        {
            await _roundManager.Initialize();
        }

        // Spawn _TutorialManagerPrefab
        GameObject tutorialManagerObj = ObjectPoolManager.SpawnPooledObject(_TutorialManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
        _tutorialManager = tutorialManagerObj.GetComponent<TutorialManager>();
        if (_tutorialManager != null)
        {
            await _tutorialManager.Initialize();
        }
    }
}
