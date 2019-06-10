using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputsController : MonoBehaviour
{
    private PlayerAnimationsController playerAnimation;
    private bool crouched;
    public PlayerController playerController;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimationsController>();
    }

    private void Update()
    {
        playerController.Move(Input.GetAxisRaw("Horizontal"));

        if (Input.GetButtonDown("Jump"))
        {
            if (!crouched)
            {
                playerController.Jump();
            }
            else
            {
                playerController.PassThroughPlatform();
            }
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (!playerController.GetGrounded())
            {
                playerAnimation.SetCrouch(false);
                return;
            }

            playerAnimation.SetCrouch(true);
            playerController.DisableControls();
            crouched = true;
        }
        else if (Input.GetAxisRaw("Vertical") > -1)
        {
            if (crouched)
            {
                crouched = false;
                playerController.EnableControls();
            }

            playerAnimation.SetCrouch(false);
            
        }
    }
}
