using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float horizontal = 0f;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpingPower = 14f;
    [SerializeField] private AudioSource jumpSoundEffect;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private bool hasPowerKiwi = false;
    private bool hasDoubleJump = false;
    private int remainingJumps = 1; // Allow one jump initially

    // Additional variable to store facing direction
    private int facingDirection = 1; // 1 represents right, -1 represents left

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        // Update facingDirection based on player input
        if (horizontal > 0f)
        {
            facingDirection = 1;
        }
        else if (horizontal < 0f)
        {
            facingDirection = -1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump();
            }
            else if (hasDoubleJump && remainingJumps > 0)
            {
                Jump();
                remainingJumps--;
            }
        }

        if (Input.GetButtonDown("Dash") && canDash && hasPowerKiwi)
        {
            StartCoroutine(Dash(false)); // Pass false to indicate regular dash
        }

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (IsGrounded())
        {
            remainingJumps = 1; // Reset jumps when grounded

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

    private void Jump()
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    private IEnumerator Dash(bool isUpwardDash)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dashDirection = isUpwardDash ? 1f : facingDirection;

        // Set the upward velocity for an upward dash
        float verticalVelocity = isUpwardDash ? dashingPower : 0f;

        rb.velocity = new Vector2(dashDirection * dashingPower, verticalVelocity);

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dash Fruit"))
        {
            hasPowerKiwi = true;
            Destroy(collision.gameObject); // Assuming you want to destroy the PowerKiwi when collected
        }
        else if (collision.CompareTag("Jump Fruit"))
        {
            hasDoubleJump = true;
            Destroy(collision.gameObject); // Assuming you want to destroy the JumpFruit when collected
        }
        
    }

    private enum MovementState { idle, running, jumping, falling }
}
