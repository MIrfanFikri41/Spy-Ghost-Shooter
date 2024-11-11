using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform armTransform; // Reference to the arm or weapon

    private Vector2 moveInput;
    private Vector2 mousePosition;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private bool isFlipped = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAim(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }

    void Update()
    {
        // Move the player
        Vector2 moveVelocity = moveInput * moveSpeed;
        rb.velocity = moveVelocity;

        // Convert mouse position to world space
        Vector3 screenPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector3 aimDirection = (screenPosition - armTransform.position).normalized;

        // Calculate the angle to rotate the weapon towards the mouse
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Flip player sprite based on mouse position relative to player position
        if (screenPosition.x < transform.position.x && !isFlipped)
        {
            FlipPlayer(true);
        }
        else if (screenPosition.x >= transform.position.x && isFlipped)
        {
            FlipPlayer(false);
        }

        // Adjust the arm rotation to point at the mouse
        armTransform.rotation = Quaternion.Euler(0, 0, isFlipped ? angle + 180f : angle);
    }

    // Function to flip the player and set the flip state
    private void FlipPlayer(bool flip)
    {
        isFlipped = flip;
        Vector3 localScale = transform.localScale;
        localScale.x = flip ? -1 : 1;
        transform.localScale = localScale;
    }
}