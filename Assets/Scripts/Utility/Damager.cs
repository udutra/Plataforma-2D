using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int power; //1

    [Header("Shake Value")]
    public float powerValue; //50, 0.5f
    public float duration, frameFreezeTime; //0.5f


    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(power, transform.position.x);
            Shaker.instance.SetValues(powerValue, duration, frameFreezeTime);
        }
    }
}
