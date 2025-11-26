using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerSetting : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera playerCamera;
    private float xRotation = 0f;
    public float xSensitivity = 40f;
    public float ySensitivity = 40f;

    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InputActionReference interact;
    private float rayDistance = 10f;
    private UnityEngine.InputSystem.PlayerInput playerInput;

    public async Task Initialize()
    {
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        playerInput.actions.Enable();
        Debug.Log("Player initialized successfully");

        await Task.CompletedTask;
    }

    void OnEnable()
    {
        interact.action.started += OnInteractStarted;
    }

    void OnDisable()
    {
        interact.action.started -= OnInteractStarted;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, interactableMask))
        {
            if (hit.collider.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.BaseInteract();
            }
        }
    }

    void Update()
    {
        HandleInteractionRaycast();
    }

    private void HandleInteractionRaycast()
    {
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);

        if (Physics.Raycast(ray, out RaycastHit pointInfo, rayDistance, interactableMask))
        {
            if (pointInfo.collider.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.BasePoint();
            }
        }
    }
}