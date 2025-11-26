using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    [Header("Addressable Keys")]
    [SerializeField] private string settingsPanelKey = "SettingsPanel";
    [SerializeField] private string crosshairCanvasKey = "CrosshairCanvas";

    private AsyncOperationHandle<GameObject> settingsHandle;
    private AsyncOperationHandle<GameObject> crosshairHandle;
    private GameObject instantiatedSettingsPanel;
    private GameObject instantiatedCrosshairCanvas;

    private static SettingsPanelController _instance;
    public static SettingsPanelController Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        // Start with crosshair ON
        _ = ShowCrosshairAsync();
    }

    public async void OpenSettings()
    {
        // HIDE CROSSHAIR
        HideCrosshair();

        if (instantiatedSettingsPanel != null)
        {
            instantiatedSettingsPanel.SetActive(true);
            return;
        }

        // LOAD & SPAWN SETTINGS PANEL
        var handle = Addressables.InstantiateAsync(settingsPanelKey);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[SettingsPanel] Failed to load: {handle.OperationException}");
            ShowCrosshair(); // Restore on fail
            return;
        }

        instantiatedSettingsPanel = handle.Result;
        SetupCanvas(instantiatedSettingsPanel);
        instantiatedSettingsPanel.SetActive(true);
        settingsHandle = handle;
    }

    public void CloseSettings()
    {
        if (instantiatedSettingsPanel != null)
            instantiatedSettingsPanel.SetActive(false);

        // SHOW CROSSHAIR
        ShowCrosshair();
    }

    private async System.Threading.Tasks.Task ShowCrosshairAsync()
    {
        if (instantiatedCrosshairCanvas != null)
        {
            instantiatedCrosshairCanvas.SetActive(true);
            return;
        }

        var handle = Addressables.InstantiateAsync(crosshairCanvasKey);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"[Crosshair] Failed to load: {handle.OperationException}");
            return;
        }

        instantiatedCrosshairCanvas = handle.Result;
        SetupCanvas(instantiatedCrosshairCanvas);
        instantiatedCrosshairCanvas.SetActive(true);
        crosshairHandle = handle;
    }

    private void ShowCrosshair()
    {
        if (instantiatedCrosshairCanvas != null)
            instantiatedCrosshairCanvas.SetActive(true);
    }

    private void HideCrosshair()
    {
        if (instantiatedCrosshairCanvas != null)
            instantiatedCrosshairCanvas.SetActive(false);
    }

    private void SetupCanvas(GameObject canvasObj)
    {
        var canvas = canvasObj.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = canvasObj.name.Contains("Settings") ? 100 : 50;
        }

        if (canvasObj.GetComponent<GraphicRaycaster>() == null)
            canvasObj.AddComponent<GraphicRaycaster>();
    }

    public void Unload()
    {
        if (settingsHandle.IsValid())
        {
            Addressables.ReleaseInstance(settingsHandle);
            instantiatedSettingsPanel = null;
        }
        if (crosshairHandle.IsValid())
        {
            Addressables.ReleaseInstance(crosshairHandle);
            instantiatedCrosshairCanvas = null;
        }
    }

    public bool IsOpen()
    {
        return instantiatedSettingsPanel != null && instantiatedSettingsPanel.activeSelf;
    }

    private void OnDestroy()
    {
        if (_instance == this) _instance = null;
        Unload();
    }
}