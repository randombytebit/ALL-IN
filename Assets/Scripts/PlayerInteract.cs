using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InputActionReference interact;
    private float rayDistance = 10f;
    private UnityEngine.InputSystem.PlayerInput playerInput;
    

    void Start()
    {
        PlayerLook playerLook = GetComponent<PlayerLook>();
        playerCamera = playerLook.playerCamera;
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        Debug.Log("PlayerInteract initialized successfully");
        playerInput.actions.Enable();
    }

    void OnEnable()
    {
        interact.action.started += OnInteractStarted;
    }

    void OnDisable()
    {
        interact.action.started -= OnInteractStarted;
    }

    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        // Initialize the ray from the camera position in the direction it is facing
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);

        // Check is the user is pointing at an interactable object
        if (Physics.Raycast(ray, out RaycastHit pointInfo, rayDistance, interactableMask))
        {
            if (pointInfo.collider.GetComponent<Interactable>() == null)
            {
                return;
            }

            // Call pointing object Basepointing method
            Interactable interactable = pointInfo.collider.GetComponent<Interactable>();
            // If the user clicks the interact button while pointing at an interactable object

            if (interactable.GetComponent<MenuPokerCard>() != null)
            {
                // Call the interactable's BaseInteract method
                interactable.BaseInteract();
            }
        }
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
        }
    }
}