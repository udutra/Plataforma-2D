using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChomper : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    private bool onGround;
    private float nextAttack;
    
    [Header("Movimentação")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float speed;

    [Header("Flip by Time")]
    public bool flipByTime;
    public float flipTime;

    [Header("Ataque")]
    public float attackRate;
    public float attackDistance;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (flipByTime)
        {
            InvokeRepeating("Flip", flipTime, flipTime);
        }
    }

    private void Update()
    {

        if (flipByTime)
        {
            return;
        }

        onGround = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);

        if (!onGround)
        {
            Flip();
        }

        CheckTarget();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * speed;
    }

    private void CheckTarget()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        float dir = player.transform.position.x - transform.position.x; //- Esquerda / + Direita

        if (distance < attackDistance)
        {
            Attack(dir);
        }
    }

    private void Attack(float dir)
    {
        if (speed < 0 && dir < 0 || speed > 0 && dir > 0) //Indo para esquerda e jogador esta na esquerda -- Indo para a direita e o jogador esta a direita
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                anim.SetTrigger("Attack");
            }
        }
    }

    private void Flip()
    {
        speed *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}