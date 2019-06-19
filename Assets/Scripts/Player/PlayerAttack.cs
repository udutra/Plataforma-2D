using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private AudioManager audioManager;
    private bool canAttack;
    private float nextFire;

    [Header("Gun")]
    public Rigidbody2D bulletPrefab;
    public Transform shotSpawn;
    public float fireRate; //0.25f
    public float shotImpulse; //10

    [Header("Melee")]
    public float attackRate;
    public UnityEvent OnAttack, ReleaseAttack;

    [Header("Sounds SFX")]
    public AudioClip meleeSfx;
    public AudioClip ShotSfx;


    private void Awake()
    {
        //attackRate = 0.5f;
        canAttack = true;
        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        audioManager = GetComponent<AudioManager>();
    }

    public void Fire()
    {

        if (!PlayerSkills.instance.skills.Contains(Skills.Gun))
        {
            return;
        }

        if (Time.time > nextFire)
        {
            audioManager.PlayAudio(ShotSfx);
            nextFire = Time.time + fireRate;
            Rigidbody2D newBullet = Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
            newBullet.AddForce(transform.right * shotImpulse, ForceMode2D.Impulse);
        }
    }

    public void MeleeAttack()
    {
        if (!PlayerSkills.instance.skills.Contains(Skills.Melee))
        {
            return;
        }

        if (canAttack)
        {
            audioManager.PlayAudio(meleeSfx);
            canAttack = false;
            OnAttack.Invoke();
            playerAnimation.SetMeleeAttack();
            if (!playerController.IsOnIce())
            {
                playerController.DisableControls();
            }
            Invoke("FinishAttack", attackRate);
        }
        
    }

    private void FinishAttack()
    {
        canAttack = true;
        ReleaseAttack.Invoke();
    }
}
