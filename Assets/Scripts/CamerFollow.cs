using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public Transform player;   // Drag your player GameObject here in the Inspector
    public Vector3 offset = new Vector3(0, 0, -10);  // Offset to keep the camera behind the player
    public float smoothSpeed = 0.125f;  // Smoothing factor for camera movement

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset; // Calculate desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smooth movement
            transform.position = smoothedPosition; // Set camera position
        }
    }
}
