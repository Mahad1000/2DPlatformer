using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedEnemy : MonoBehaviour
{
    private bool isFalling = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Make sure it's initially not affected by physics
    }

    private void Update()
    {
        // Check if the player is underneath the spiked face
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && player.transform.position.x > transform.position.x - 1.0f &&
            player.transform.position.x < transform.position.x + 1.0f)
        {
            // If the player is underneath, start falling
            isFalling = true;
            rb.isKinematic = false; // Enable physics to make it fall
        }

        // Check if the spiked face has fallen off the screen, then you can destroy it or handle it as needed.
        if (isFalling && transform.position.y < -20f)
        {
            Destroy(gameObject); // For example, you might want to destroy it when it falls off the screen.
        }
    }
}

