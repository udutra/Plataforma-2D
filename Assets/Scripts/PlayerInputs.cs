using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerController playerController;

    private void Update()
    {
        playerController.Move(Input.GetAxisRaw("Horizontal"));
    }
}
