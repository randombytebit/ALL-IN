using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

public class MenuInitiator : MonoBehaviour
{
    [Header("=== Prefabs to Spawn ===")]
    [SerializeField] private GameObject _objectPoolManagerPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _directionalLightPrefab;
    [SerializeField] private GameObject _textMeshProPrefab;
    [SerializeField] private GameObject _gameManagerPrefab;
    [SerializeField] private List<GameObject> _menuPokerCardPrefabs;

    private PlayerInput _inputActions;
    private bool _hasInitialized = false;
    private float _cardXOffset = -1.25f;

    private void OnEnable()
    {
        Debug.Log("[MenuInit] OnEnable");
        SceneManager.sceneLoaded += OnSceneLoaded;

        _inputActions = new PlayerInput();
        _inputActions.UI.SettingsToggle.performed += OnSettingsToggle;
        _inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("[MenuInit] OnDisable");
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

            GameObject textObj = Spawn(_textMeshProPrefab, Vector3.zero, Quaternion.Euler(90f, 0f, 0f), ObjectPoolManager.ObjectPoolType.ManagerObject);
            TextMeshPro textMesh = textObj?.GetComponent<TextMeshPro>();

            foreach (GameObject cardPrefab in _menuPokerCardPrefabs)
            {
                _cardXOffset += 0.25f;
                GameObject cardObj = Spawn(cardPrefab, new Vector3(_cardXOffset, 2.56f, -0.83f), Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
                
                if (cardObj != null && textMesh != null)
                {
                    if (cardObj.TryGetComponent<MenuPokerCard>(out var menuCard))
                    {
                        menuCard.SetTextMeshPro(textMesh);
                        _ = menuCard.Initialize(); // Fire and forget
                    }
                }
            }

            GameObject gmObj = Spawn(_gameManagerPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.ManagerObject);
            if (gmObj != null)
            {
                GameManager gm = gmObj.GetComponent<GameManager>();
                if (gm != null)
                {
                    await gm.Initialize();
                }
            }

            _hasInitialized = true;
            Debug.Log("[MenuInit] ALL OBJECTS SPAWNED — GameManager takes control");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[MenuInit] SPAWN FAILED: {ex}");
        }
    }

    private GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot, ObjectPoolManager.ObjectPoolType type)
    {
        if (prefab == null)
        {
            Debug.LogWarning($"[MenuInit] Missing prefab for {type}");
            return null;
        }

        GameObject obj = ObjectPoolManager.SpawnPooledObject(prefab, pos, rot, type);
        if (obj != null) DontDestroyOnLoad(obj);
        return obj;
    }
}