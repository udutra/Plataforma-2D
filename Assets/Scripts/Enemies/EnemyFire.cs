using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private Animator anim;
    private AudioManager audioManager;

    [Header("Attack")]
    public float fireRate;
    public Rigidbody2D bulletPrefab;
    public Transform shotSpawn;
    public Vector2 shotImpulse;

    [Header("Sounds SFX")]
    public AudioClip openMouthSFX;
    public AudioClip shotSFX;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        InvokeRepeating("SetFire", fireRate, fireRate);
    }

    private void SetFire()
    {
        audioManager.PlayAudio(openMouthSFX);
        anim.SetTrigger("Fire");
    }

    private void Fire()
    {
        audioManager.PlayAudio(shotSFX);
        Rigidbody2D newBullet = Instantiate(bulletPrefab, shotSpawn.position, transform.rotation);
        newBullet.AddForce(shotImpulse, ForceMode2D.Impulse);
    }
}
