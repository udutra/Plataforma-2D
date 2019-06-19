using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject bulletImpact;
    public AudioClip impactSFX;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.PlayAudioAtPoint(transform.position, impactSFX);
        GameObject newImpact = Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(newImpact, 1);
        Destroy(gameObject);
    }
}
