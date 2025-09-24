using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown dropdown;

    public void GetValue()
    {
        int index = dropdown.value;
        string value = dropdown.options[index].text;
        Debug.Log($"Selected Dropdown Value: {value}");
    }

    public void SetDisplayMode()
    {
        string selectedText = dropdown.options[dropdown.value].text;
        switch (selectedText)
        {
            case "WINDOW FULLSCREEN":
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case "BORDERLESS WINDOWED":
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case "WINDOWED":
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                Debug.LogWarning("Invalid dropdown value for display mode.");
                break;
        }
    }

    public void SetResolution()
    {
        string selectedText = dropdown.options[dropdown.value].text;
        string[] dimensions = selectedText.Split('X');
        if (dimensions.Length == 2 &&
            int.TryParse(dimensions[0].Trim(), out int width) &&
            int.TryParse(dimensions[1].Trim(), out int height))
        {
            Screen.SetResolution(width, height, Screen.fullScreenMode);
            Debug.Log($"Resolution set to: {width}x{height}");
        }
        else
        {
            Debug.LogWarning("Invalid dropdown value for resolution.");
        }
    }

    public void SetQuality()
    {
        string selectedText = dropdown.options[dropdown.value].text;
        switch (selectedText.ToUpper())
        {
            case "LOW":
                QualitySettings.SetQualityLevel(0, true);
                Debug.Log("Quality set to LOW");
                break;
            case "MEDIUM":
                QualitySettings.SetQualityLevel(1, true);
                Debug.Log("Quality set to MEDIUM");
                break;
            case "HIGH":
                QualitySettings.SetQualityLevel(2, true);
                Debug.Log("Quality set to HIGH");
                break;
            default:
                Debug.LogWarning($"Invalid quality setting: {selectedText}");
                break;
        }
    }

    public void SetTextureQuality()
    {
        string selectedText = dropdown.options[dropdown.value].text;
        switch (selectedText.ToUpper())
        {
            case "LOW":
                QualitySettings.globalTextureMipmapLimit = 2;
                Debug.Log("Texture quality set to LOW");
                break;
            case "MEDIUM":
                QualitySettings.globalTextureMipmapLimit = 1;
                Debug.Log("Texture quality set to MEDIUM");
                break;
            case "HIGH":
                QualitySettings.globalTextureMipmapLimit = 0;
                Debug.Log("Texture quality set to HIGH");
                break;
            default:
                Debug.LogWarning($"Invalid texture quality setting: {selectedText}");
                break;
        }
    }

    public void SetShadowQuality()
    {
        string selectedText = dropdown.options[dropdown.value].text;
        switch (selectedText.ToUpper())
        {
            case "LOW":
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowDistance = 50f;
                Debug.Log("Shadow quality set to LOW");
                break;
            case "MEDIUM":
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                QualitySettings.shadowDistance = 75f;
                Debug.Log("Shadow quality set to MEDIUM");
                break;
            case "HIGH":
                QualitySettings.shadowResolution = ShadowResolution.High;
                QualitySettings.shadowDistance = 100f;
                Debug.Log("Shadow quality set to HIGH");
                break;
            default:
                Debug.LogWarning($"Invalid shadow quality setting: {selectedText}");
                break;
        }
    }
}
