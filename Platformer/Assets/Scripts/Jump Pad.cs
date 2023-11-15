using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private float bounce = 30f;
    private Animator animator;

    private void Start()
    {
        // Get the Animator component on the Bounce Pad
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            // Trigger the bounce animation
            animator.SetTrigger("BounceTrigger");

            // Apply the bounce force to the player
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
    
}
