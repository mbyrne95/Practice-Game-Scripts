using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public GameObject xp;

    public float speed;
    public int health, maxHealth = 10;

    public float attackRadius;
    public bool shouldRotate;

    public LayerMask whatIsPlayer;
    public GameObject player;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector3 dir;

    private Collider2D col;

    private bool isInAttackRange;

    //private Vector3 startingPosition;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        int targetHealth = target.GetComponent<Health>().health;
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        //isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }


        //contact damage
        if (col.IsTouching(player.GetComponent<Collider2D>()))
        {
            player.GetComponent<Health>().TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {
        if (col.gameObject.tag == "XP")
        {
            Physics2D.IgnoreCollision(xp.GetComponent<Collider2D>(), col);
        }

        MoveCharacter(movement);
        /*
        if (!isInAttackRange)
        {
            MoveCharacter(movement);
        }
        else        
        {
            rb.velocity = Vector2.zero;
        }
        */
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    private void ContactDamage(int targetHealth)
    {
        targetHealth = targetHealth - 1;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        //Vector2 transform = new Vector2(rb.x, rb.y);

        if (health <= 0)
        {
            Instantiate(xp, rb.transform.position, rb.transform.rotation);
            Destroy(gameObject);
        }
    }

}
