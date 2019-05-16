using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 newMovement;
    private bool facingRight, jump, grounded , doubleJump;

    [Header("Dados da velocidade do PLayer")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float jumpForce;

    [Header("Dados do Colisor com o chão")]
    public Transform groundedCheck;
    public float groundRadius;
    public LayerMask groundLayer;

    private void Awake()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundedCheck.position, groundRadius, groundLayer);

        if (grounded)
        {
            doubleJump = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = newMovement;

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
}