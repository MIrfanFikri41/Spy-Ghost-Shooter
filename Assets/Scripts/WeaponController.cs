using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bulletPrefab;  // Bullet prefab
    public Transform firePoint;      // Fire point (tip of the weapon)
    public float bulletSpeed = 20f;  // Bullet speed

    void Update()
    {
        // Fire bullet when input is detected
        if (Input.GetButtonDown("Fire1"))  // Trigger fire on input
        {
            OnFire();
        }
    }

    void OnFire()
    {
        // Instantiate the bullet at the firePoint's position
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calculate direction from firePoint to mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePointPosition = firePoint.position;
        Vector2 direction = (mousePosition - firePointPosition).normalized;

        // Apply velocity to the bullet to move it towards the mouse
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }
}
