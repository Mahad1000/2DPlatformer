using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float bounceForce = 30f;
    private Animator anim;
    [SerializeField] private AudioSource bounceSound;

    private void Start()
    {
       
        anim = GetComponent<Animator>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            // Apply the bounce force to the player
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0f); // Reset vertical velocity
            playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

            // Play the bounce sound
            bounceSound.Play();
            anim.SetTrigger("JumpPad");
        }
    }

}
