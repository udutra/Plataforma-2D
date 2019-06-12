using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private Animator anim;
    public float fireRate;
    public Rigidbody2D bulletPrefab;
    public Transform shotSpawn;
    public Vector2 shotImpulse;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        InvokeRepeating("SetFire", fireRate, fireRate);
    }

    private void SetFire()
    {
        anim.SetTrigger("Fire");
    }

    private void Fire()
    {
        Rigidbody2D newBullet = Instantiate(bulletPrefab, shotSpawn.position, transform.rotation);
        newBullet.AddForce(shotImpulse, ForceMode2D.Impulse);
    }
}
