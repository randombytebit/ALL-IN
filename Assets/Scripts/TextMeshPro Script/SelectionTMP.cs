using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTMP : MonoBehaviour
{
    void Start()
    {
        Debug.Log("SelectionTMP script started.");
        Vector3 currentPosition = transform.position;
        currentPosition.x += 0.5f;
        transform.position = currentPosition;
    }
}
