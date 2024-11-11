using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseZone : MonoBehaviour
{
    public PlayerSwitchManager playerSwitchManager; // Reference to the PlayerSwitchManager
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player that entered is the Human character
        if (other.CompareTag("PlayerHuman"))
        {
            // Notify PlayerSwitchManager that Human is in the base zone
            playerSwitchManager.SetCanSwitchToGhost(true);
            Debug.Log("Human entered the Main Base Zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player that exited is the Human character
        if (other.CompareTag("PlayerHuman"))
        {
            // Notify PlayerSwitchManager that Human left the base zone
            playerSwitchManager.SetCanSwitchToGhost(false);
            Debug.Log("Human left the Main Base Zone");
        }
    }
}
