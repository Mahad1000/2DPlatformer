using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float horizontal = 0f; // Changed the variable name to match your code
    [SerializeField] private float speed = 7f; // Changed the variable name to match your code
    [SerializeField] private float jumpingPower = 14f; // Changed the variable name to match your code

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() // create another update called fixed update only for collision detection.
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
{
    MovementState state;

    if (IsGrounded())
    {
        if (horizontal > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (horizontal < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
    }
    else // Character is in the air
    {
        if (horizontal > 0f)
        {
            sprite.flipX = false; // Flip the sprite when moving to the right in the air
        }
        else if (horizontal < 0f)
        {
            sprite.flipX = true; // Flip the sprite when moving to the left in the air
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else
        {
            state = MovementState.falling;
        }
    }

    anim.SetInteger("state", (int)state);
}





    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private enum MovementState { idle, running, jumping, falling }
}
