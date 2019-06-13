using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private bool crouched;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;

    public PlayerController playerController;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
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

        if (Input.GetButtonDown("Fire1"))
        {
            playerAttack.Fire();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            playerAttack.MeleeAttack();
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
