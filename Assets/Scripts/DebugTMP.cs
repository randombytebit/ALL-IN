using UnityEngine;

public class DebugTMP : MonoBehaviour
{    void Start()
    {
        Debug.Log(transform.position);
        Vector3 currentPosition = transform.position;
        currentPosition.x += 0.5f;
        transform.position = currentPosition;
    }
}
