using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;  // How long the bullet lasts before being destroyed

    void Start()
    {
        // Destroy the bullet after a certain time to prevent it from existing forever
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy bullet on impact with any object
        Destroy(gameObject);
    }
}
