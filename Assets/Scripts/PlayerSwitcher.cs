using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject ghostPlayer;
    public GameObject humanPlayer;
    public Collider2D mainBaseZone;
    public CameraFollow cameraFollow;  // Reference to the CameraFollow script

    private bool canSwitchToHuman = false;
    private bool canSwitchToGhost = false;
    private bool isGhostActive = true;

    void Start()
    {
        SetActiveCharacter(true);   // Start with Ghost as the active character
    }

    void Update()
    {
        // Check if the player can switch to Human or Ghost
        if (isGhostActive && canSwitchToHuman && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToHuman();
        }
        else if (!isGhostActive && canSwitchToGhost && Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToGhost();
        }
    }

    private void SetActiveCharacter(bool setGhostActive)
    {
        isGhostActive = setGhostActive;

        // Enable only the active character and its components
        ghostPlayer.SetActive(setGhostActive);
        humanPlayer.SetActive(!setGhostActive);

        // Update the camera target to follow the active character
        cameraFollow.SetTarget(setGhostActive ? ghostPlayer.transform : humanPlayer.transform);
    }

    private void SwitchToHuman()
    {
        SetActiveCharacter(false);
    }

    private void SwitchToGhost()
    {
        SetActiveCharacter(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isGhostActive)
            {
                canSwitchToHuman = true;
            }
            else
            {
                canSwitchToGhost = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isGhostActive)
            {
                canSwitchToHuman = false;
            }
            else
            {
                canSwitchToGhost = false;
            }
        }
    }
}
