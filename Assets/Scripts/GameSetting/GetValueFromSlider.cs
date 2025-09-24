using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetValueFromSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI valueText;

    public void SetValueText()
    {
        float value = slider.value;
        valueText.text = value.ToString("0");
        Debug.Log($"Slider Value: {value}");
    }

    public void SetMaxframe()
    {
        float value = slider.value;
        int frameRate = Mathf.RoundToInt(value);

        Application.targetFrameRate = frameRate;
        Debug.Log($"Frame rate limited to: {frameRate} FPS");
    }

    public void SetMasterVolume()
    {
        float value = slider.value;
        AudioListener.volume = value;
        Debug.Log($"Master Volume set to: {AudioListener.volume * 100}%");
    }

    // public void SetSFXVolume()
    // {

    // }
    
    // public void SetVoiceVolume()
    // {

    // }
}
