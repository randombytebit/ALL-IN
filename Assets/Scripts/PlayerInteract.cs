using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private LayerMask interactableMask;
    private float rayDistance = 10f;

    void Start()
    {
        playerCamera = GetComponent<PlayerLook>().playerCamera;
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, rayDistance, interactableMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() == null)
            {
                return;
            }
            
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            interactable.BaseInteract();
            // Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
        }
    }
}
