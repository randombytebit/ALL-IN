using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class TutorialInitiator : MonoBehaviour
{
    [Header("=== Prefabs to Spawn ===")]
    [SerializeField] private GameObject _objectPoolManagerPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _directionalLightPrefab;
    [SerializeField] private GameObject _playerManagerPrefab;
    [SerializeField] private GameObject _deckManagerPrefab;
    [SerializeField] private GameObject _pokerLogicManagerPrefab;
    [SerializeField] private GameObject _turnManagerPrefab;
    [SerializeField] private GameObject _bettingManagerPrefab;
    [SerializeField] private GameObject _roundManagerPrefab;
    [SerializeField] private GameObject _tutorialManagerPrefab;

    private PlayerInput _inputActions;
    private bool _hasInitialized = false;

    private void OnEnable()
    {
        Debug.Log("[TutorialInit] OnEnable");
        SceneManager.sceneLoaded += OnSceneLoaded;

        _inputActions = new PlayerInput();
        _inputActions.UI.SettingsToggle.performed += OnSettingsToggle;
        _inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("[TutorialInit] OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (_inputActions != null)
        {
            _inputActions.UI.SettingsToggle.performed -= OnSettingsToggle;
            _inputActions.UI.Disable();
            _inputActions.Dispose();
        }
    }

    private void OnSettingsToggle(InputAction.CallbackContext context)
    {
        if (SettingsPanelController.Instance == null) return;

        if (SettingsPanelController.Instance.IsOpen())
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SettingsPanelController.Instance.CloseSettings();
        }
        else
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SettingsPanelController.Instance.OpenSettings();
        }
    }

    private async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_hasInitialized) return;

        try
        {
            if (ObjectPoolManager.Instance == null)
            {
                GameObject poolObj = Instantiate(_objectPoolManagerPrefab);
                DontDestroyOnLoad(poolObj);
                await Task.Yield(); 
            }

            Spawn(_playerPrefab,           new Vector3(-0.02f, 2.58f, -5.21f), Quaternion.identity,        ObjectPoolManager.ObjectPoolType.PlayerObject);
            Spawn(_directionalLightPrefab, new Vector3(0.77f, 13.21f, 1.37f),   Quaternion.Euler(60f, 0f, 0f), ObjectPoolManager.ObjectPoolType.ManagerObject);
            Spawn(_playerManagerPrefab,    Vector3.zero,                      Quaternion.identity,        ObjectPoolManager.ObjectPoolType.ManagerObject);
            Spawn(_deckManagerPrefab,      Vector3.zero,                      Quaternion.identity,        ObjectPoolManager.ObjectPoolType.ManagerObject);
            Spawn(_pokerLogicManagerPrefab,Vector3.zero,                      Quaternion.identity,        ObjectPoolManager.ObjectPoolType.ManagerObject);
            Spawn(_turnManagerPrefab,      Vector3.zero,                      Quaternion.identity,        ObjectPoolManager.ObjectPoolType.ManagerObject);
            Spawn(_bettingManagerPrefab,   Vector3.zero,                      Quaternion.identity,        ObjectPoolManager.ObjectPoolType.ManagerObject);
            Spawn(_roundManagerPrefab,     Vector3.zero,                      Quaternion.identity,        ObjectPoolManager.ObjectPoolType.ManagerObject);
            Spawn(_tutorialManagerPrefab,  Vector3.zero,                      Quaternion.identity,        ObjectPoolManager.ObjectPoolType.ManagerObject);

            _hasInitialized = true;
            Debug.Log("[TutorialInit] ALL OBJECTS SPAWNED — TutorialManager takes control");

            TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager != null)
            {
                await tutorialManager.Initialize();
                tutorialManager.TutorialStart();
            }
            else
            {
                Debug.LogError("[TutorialInit] TutorialManager not found after spawn!");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[TutorialInit] SPAWN FAILED: {ex}");
        }
    }

    private void Spawn(GameObject prefab, Vector3 pos, Quaternion rot, ObjectPoolManager.ObjectPoolType type)
    {
        if (prefab == null)
        {
            Debug.LogWarning($"[TutorialInit] Missing prefab for {type}");
            return;
        }

        ObjectPoolManager.SpawnPooledObject(prefab, pos, rot, type);
    }
}