using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerLook playerLook;
    private PlayerInput.OnFootActions onFoot;
    private PlayerInput.InGameActions inGame;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.onFoot;
        inGame = playerInput.inGame;

        playerLook = GetComponent<PlayerLook>();
        
    }

    void LateUpdate()
    {
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
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
