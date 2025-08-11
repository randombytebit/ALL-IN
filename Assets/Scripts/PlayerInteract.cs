using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private LayerMask interactableMask;
    private float rayDistance = 10f;
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    // [SerializeField] private AnimationHelper animationHelper;

    void Start()
    {
        playerCamera = GetComponent<PlayerLook>().playerCamera;
        playerInput = GetComponent<PlayerInput>();
        onFoot = playerInput.onFoot;
        // AnimationHelper.GameState currentState = animationHelper.GetCurrentState();
    }

    void Update()
    {
        // Initialize the ray from the camera position in the direction it is facing
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);

        // Check is the user is pointing at an interactable object
        if (Physics.Raycast(ray, out RaycastHit pointInfo, rayDistance, interactableMask))
        {
            if (pointInfo.collider.GetComponent<Interactable>() == null)
            {
                return;
            }

            // Call pointing object Basepointing method
            Interactable interactable = pointInfo.collider.GetComponent<Interactable>();
            interactable.BasePoint();

            // If the user clicks the interact button while pointing at an interactable object
            if (onFoot.Interact.triggered)
            {
                Debug.Log("Interacting with: " + interactable.name);
            }
        }
    }
}