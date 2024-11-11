using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ghostPlayer;        // Reference to the Ghost Player
    public Transform humanPlayer;        // Reference to the Human Player
    public float followSpeed = 5f;       // Speed at which the camera follows the player
    public Vector3 offset = new Vector3(0, 0, -1);  // Camera offset (adjust as needed)

    private Transform currentTarget;     // The current target the camera is following

    void Start()
    {
        // Initially set the camera to follow the Ghost
        currentTarget = ghostPlayer;
    }

    void Update()
    {
        // Switch the camera target when the player switches characters
        if (currentTarget != null)
        {
            Vector3 desiredPosition = currentTarget.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.position = smoothPosition;
        }
    }

    // Method to switch the camera target (called from PlayerSwitchManager)
    public void SwitchCameraTarget(bool isGhostActive)
    {
        if (isGhostActive)
        {
            currentTarget = ghostPlayer;
        }
        else
        {
            currentTarget = humanPlayer;
        }
    }
}
