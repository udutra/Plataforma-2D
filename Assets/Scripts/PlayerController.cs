using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float walkSpeed;
    private Vector2 newMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = newMovement;
    }

    public void Move(float direction)
    {
        float currentSpeed = walkSpeed;

        newMovement = new Vector2(direction * currentSpeed, rb.velocity.y);
    }
}
