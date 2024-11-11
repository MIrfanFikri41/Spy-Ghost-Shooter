using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchManager : MonoBehaviour
{
    public GameObject PlayerGhost;           // Reference to Ghost Player GameObject
    public GameObject PlayerHuman;           // Reference to Human Player GameObject
    public int pointsRequired = 100;         // Points required for Ghost to switch to Human
    public Transform mainBasePosition;       // Reference to main base position for Human to switch back to Ghost

    private bool isGhostActive = true;       // Tracks if Ghost is currently active
    private bool canSwitchToGhost = false;  // Whether the Human can switch to Ghost (based on Main Base Zone)
    private int playerPoints = 0;            // Tracks the points of the Ghost player

    void Start()
    {
        // Start with Ghost active and Human inactive
        SetActiveCharacter(isGhost: true);
    }

    void Update()
    {
        if (isGhostActive && playerPoints >= pointsRequired && Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToHuman();
        }
        else if (!isGhostActive && canSwitchToGhost && Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToGhost();
        }
    }

    // Sets active character and disables the other
    private void SetActiveCharacter(bool isGhost)
    {
        isGhostActive = isGhost;

        // Enable Ghost scripts, disable Human scripts if Ghost is active, and vice versa
        ToggleCharacterScripts(PlayerGhost, isGhost);
        ToggleCharacterScripts(PlayerHuman, !isGhost);

        // Toggle visibility
        PlayerGhost.SetActive(isGhost);
        PlayerHuman.SetActive(!isGhost);
    }

    private void ToggleCharacterScripts(GameObject character, bool isActive)
    {
        // Enable or disable the PlayerController based on active status
        PlayerController playerController = character.GetComponent<PlayerController>();
        if (playerController != null) playerController.enabled = isActive;

        // Enable or disable the Arm and Weapon Controller based on active status
        Transform arm = character.transform.Find("Arm");
        if (arm != null)
        {
            Transform weapon = arm.Find("Weapon");
            if (weapon != null)
            {
                WeaponController weaponController = weapon.GetComponent<WeaponController>();
                if (weaponController != null)
                    weaponController.enabled = isActive; // Toggle WeaponController on or off
            }
        }
    }

    private void SwitchToHuman()
    {
        // Activate Human, deactivate Ghost
        SetActiveCharacter(isGhost: false);
        Debug.Log("Switched to Human!");
    }

    private void SwitchToGhost()
    {
        // Activate Ghost, deactivate Human
        SetActiveCharacter(isGhost: true);
        Debug.Log("Switched to Ghost!");
    }

    public void SetCanSwitchToGhost(bool value)
    {
        canSwitchToGhost = value;
    }

    // Call this method to add points to the Ghost player
    public void AddPoints(int points)
    {
        playerPoints += points;
        Debug.Log("Points: " + playerPoints);
    }
}
