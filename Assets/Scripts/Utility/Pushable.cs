using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Rigidbody2D rb;
    public float defaultMass; //25

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetPushing(true);
            other.GetComponent<PlayerAnimation>().SetPush(true);
            rb.mass = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().SetPushing(false);
            other.GetComponent<PlayerAnimation>().SetPush(false);
            rb.mass = defaultMass;
        }
    }
}
