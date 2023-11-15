using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFruit : MonoBehaviour
{
    public float dashDuration = 1.5f; // Adjust the dash duration as needed

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // Trigger dash ability in the player script
                player.ActivateDash(dashDuration);

                // Optionally, play a sound or particle effect for feedback
                // AudioManager.PlayDashSound();
                // Instantiate(dashEffectPrefab, transform.position, Quaternion.identity);

                // Destroy the dash fruit GameObject
                Destroy(gameObject);
            }
        }
    }
}
