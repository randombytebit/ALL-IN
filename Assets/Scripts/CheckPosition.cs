using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    private void Awake()
    {
        // Debug GameObject name
        Debug.Log($"GameObject Name: {gameObject.name}");
        
        // Debug position
        Debug.Log($"Position: {transform.position}");
        
        // Debug rotation in different formats
        Debug.Log($"Rotation (euler): {transform.rotation.eulerAngles}");
        Debug.Log($"Rotation (quaternion): {transform.rotation}");
        Debug.Log($"Is identity?: {transform.rotation == Quaternion.identity}");
        
        // Compare with Quaternion.identity values
        Debug.Log($"Identity quaternion values: {Quaternion.identity}");
    }
}
