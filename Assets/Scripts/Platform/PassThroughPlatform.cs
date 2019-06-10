using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    private int myLayer;

    [SerializeField]
    private int ignoreLayer;

    private void Start()
    {
        myLayer = gameObject.layer;
    }

    public void PassingThrough()
    {
        gameObject.layer = ignoreLayer;
        Invoke("SetDefaultLayer", 0.5f);
    }

    private void SetDefaultLayer()
    {
        gameObject.layer = myLayer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.SetPlatform(this);
        }
    }
}
