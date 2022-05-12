using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D boxColl;
    public Transform frontCheck;

    private float dirX;
    private bool isTouchingFront;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private float checkRadius;
    [SerializeField] private float slideForce;
    [SerializeField] private float speedForce;

    [SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementState {  idle, run, jump, fall, sliding };

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(dirX * speedForce, rb2D.velocity.y);

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, jumpableGround);

        //Sliding
        if (IsSliding())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, - slideForce, float.MaxValue));
        }
        //Moving
        if (Input.GetButtonDown("Jump") && (IsGrounded() || isTouchingFront))
        {
            jumpSoundEffect.Play();
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f && IsGrounded())
        {
            sr.flipX = false;
            state = MovementState.run;
        }
        else if (dirX < 0f && IsGrounded())
        {
            sr.flipX = true;
            state = MovementState.run;
        } 
        else if (IsSliding())
        {
            state = MovementState.sliding;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb2D.velocity.y > .1f)
        {
            state = MovementState.jump;
        }
        else if (rb2D.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }

        anim.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private bool IsSliding()
    {
        if (isTouchingFront == true && dirX != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
