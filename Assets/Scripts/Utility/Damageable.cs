using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private int currentHealth;
    private float x;
    private bool invincible, isDead;

    public Color damageColor;
    public Vector2 impactForce;
    public int maxHealth;
    public float invincibleTime, noControleTime; //noControlTime = 0.1f
    public UnityEvent OnDamage, ReleaseDamage, onDeath;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        startColor = spriteRenderer.color; 
    }

    public void TakeDamage(int damageAmount, float xPos)
    {
        if (invincible || isDead)
        {
            return;
        }

        x = xPos;
        OnDamage.Invoke();
        invincible = true;
        Invoke("SetInvincible", invincibleTime);
        Invoke("GainControl", noControleTime);
        currentHealth -= damageAmount;

        if (gameObject.CompareTag("Player"))
        {
            UIManager.instance.SetLives(currentHealth);
        }

        if (currentHealth <= 0)
        {
            isDead = true;
            onDeath.Invoke();
        }
    }

    private void GainControl()
    {
        if (isDead)
        {
            return;
        }
        ReleaseDamage.Invoke();
    }

    public void DamageImpact()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float dir;
            if (x < transform.position.x)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }

            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(impactForce.x * dir, impactForce.y), ForceMode2D.Impulse);
        }
    }

    private void SetInvincible()
    {
        invincible = false;
    }

    public void StartDamageSprite()
    {
        StartCoroutine(DamageSprite());
    }

    private IEnumerator DamageSprite()
    {
        float timer = 0;
        while(timer < invincibleTime)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
            spriteRenderer.color = startColor;
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        spriteRenderer.color = startColor;
    }

    public void Respawn()
    {
        isDead = false;
        currentHealth = maxHealth;
        UIManager.instance.SetLives(currentHealth);
    }

    public void SetHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIManager.instance.SetLives(currentHealth);
    }

    public void DestroyObject(float time)
    {
        Destroy(gameObject, time);
    }
}
