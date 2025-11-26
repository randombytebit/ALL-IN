using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerSetting playerSetting;
    private PlayerInput.OnFootActions onFoot;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.onFoot;

        playerSetting = GetComponent<PlayerSetting>(); 
        
        if (playerSetting == null)
        {
            Debug.LogError("PlayerSetting component not found on " + gameObject.name);
            enabled = false;
            return;
        }
    }

    void LateUpdate()
    {
        if (playerSetting != null)
        {
            playerSetting.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();    
    }    
}
