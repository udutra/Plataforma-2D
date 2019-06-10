using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimationsController playerAnimation;
    private Vector2 newMovement;
    private bool facingRight, jump, grounded , doubleJump, canControl, onIce;
    private PassThroughPlatform platform;

    [Header("Dados da velocidade do Player")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpForce;
    public float iceForce;
    public float maxSpeed;

    [Header("Dados do Colisor com o chão")]
    public Transform groundedCheck;
    public float groundRadius;
    public LayerMask groundLayer;

    private void Awake()
    {
        facingRight = true;
        canControl = true;
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimationsController>();
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundedCheck.position, groundRadius, groundLayer);

        playerAnimation.SetOnGround(grounded);

        if (grounded)
        {
            doubleJump = false;
        }

        //Debug.Log(rb.velocity.y);
    }

    private void FixedUpdate()
    {
        playerAnimation.SetVSpeed(rb.velocity.y);

        if (!canControl)
        {
            return;
        }

        if (!onIce)
        {
            rb.velocity = newMovement;
        }

        else if (onIce)
        {
            rb.AddForce(new Vector2(newMovement.x * iceForce, 0), ForceMode2D.Force);
            if (Mathf.Abs(rb.velocity.x) >= maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }
        }

        if (jump)
        {
            jump = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }

        
    }

    public void Move(float direction)
    {
        float currentSpeed = walkSpeed;

        newMovement = new Vector2(direction * currentSpeed, rb.velocity.y);

        playerAnimation.SetSpeed((int) Mathf.Abs(direction));
        if (facingRight && direction < 0)
        {
            Flip();
        }
        else if(!facingRight && direction > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public void Jump()
    {
        if (grounded || !doubleJump)
        {
            jump = true;
        }
    }

    public void DisableControls()
    {
        canControl = false;
    }

    public void EnableControls()
    {
        canControl = true;
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void SetOnIce(bool state)
    {
        onIce = state;
        if (onIce)
        {
            rb.drag = 2;
        }
        else
        {
            rb.drag = 0;
        }
    }

    public void SetPlatform(PassThroughPlatform passPlatform)
    {
        platform = passPlatform;
    }

    public void PassThroughPlatform()
    {
        if (platform != null)
        {
            platform.PassingThrough();
        }
    }
}