using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingMgr : MonoBehaviour
{
    [Header("Setting Buttons")]
    [SerializeField] private Image GeneralSettingButton;
    [SerializeField] private Image GraphicsSettingButton;
    [SerializeField] private Image AudioSettingButton;

    [Header("Setting Buttons Text")]
    [SerializeField] private TextMeshProUGUI GeneralSettingText;
    [SerializeField] private TextMeshProUGUI GraphicsSettingText;
    [SerializeField] private TextMeshProUGUI AudioSettingText;

    [Header("Setting Canvases")]
    [SerializeField] private GameObject GeneralSetting;
    [SerializeField] private GameObject GraphicsSetting;
    [SerializeField] private GameObject AudioSetting;

    [Header("Button Textures")]
    [SerializeField] private Sprite InactiveTexture;
    [SerializeField] private Sprite ActiveTexture;


    private void Awake()
    {
        GeneralSetting.gameObject.SetActive(true);
        GraphicsSetting.gameObject.SetActive(false);
        AudioSetting.gameObject.SetActive(false);
        OnClickedButton(GeneralSettingButton);
    }

    // Function for Canvas Button OnClick events
    public void OpenGeneralSetting()
    {
        GeneralSetting.gameObject.SetActive(true);
        GraphicsSetting.gameObject.SetActive(false);
        AudioSetting.gameObject.SetActive(false);
        OnClickedButton(GeneralSettingButton);

        Debug.Log("Clicked General Setting Button");
    }

    public void OpenGraphicsSetting()
    {
        GeneralSetting.gameObject.SetActive(false);
        GraphicsSetting.gameObject.SetActive(true);
        AudioSetting.gameObject.SetActive(false);
        OnClickedButton(GraphicsSettingButton);

        Debug.Log("Clicked Graphics Setting Button");
    }

    public void OpenAudioSetting()
    {
        GeneralSetting.gameObject.SetActive(false);
        GraphicsSetting.gameObject.SetActive(false);
        AudioSetting.gameObject.SetActive(true);
        OnClickedButton(AudioSettingButton);

        Debug.Log("Clicked Audio Setting Button");
    }

    private void OnClickedButton(Image clickedButton)
    {
        // Reset all buttons to inactive texture
        GeneralSettingButton.sprite = InactiveTexture;
        GraphicsSettingButton.sprite = InactiveTexture;
        AudioSettingButton.sprite = InactiveTexture;

        // Set the clicked button to active texture
        clickedButton.sprite = ActiveTexture;

        // Reset all button texts to default color
        GeneralSettingText.color = Color.white;
        GraphicsSettingText.color = Color.white;
        AudioSettingText.color = Color.white;

        // Set the clicked button text to highlighted color
        if (clickedButton == GeneralSettingButton)
        {
            GeneralSettingText.color = Color.black;
        }
        else if (clickedButton == GraphicsSettingButton)
        {
            GraphicsSettingText.color = Color.black;
        }
        else if (clickedButton == AudioSettingButton)
        {
            AudioSettingText.color = Color.black;
        }
    }
}
