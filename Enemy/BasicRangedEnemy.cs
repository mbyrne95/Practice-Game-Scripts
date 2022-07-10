using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicRangedEnemy : MonoBehaviour, IEnemy
{
    public GameObject xp;

    public float speed;
    public float moveSpeedActive;
    public float health, maxHealth = 10;

    public float attackRadius;
    public bool shouldRotate;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastShootTime = 0;
    [SerializeField]
    private float fireRate;


    public LayerMask whatIsPlayer;

    
    public GameObject player;

    private Transform target;
    [HideInInspector]
    public Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    [HideInInspector]
    public Vector3 dir;

    private Collider2D col;

    private bool isInAttackRange;

    private List<Debuff> debuffs = new List<Debuff>();

    [SerializeField]
    private int silverBoltStacks = 0;

    //private float tempSpeed;

    float IEnemy.moveSpeed { get => speed; set => speed = value; }
    float IEnemy.moveSpeedActive { get => moveSpeedActive; set => moveSpeedActive = value; }


    private void Start()
    {
        health = maxHealth;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        int targetHealth = target.GetComponent<Health>().health;
        col = GetComponent<Collider2D>();
        moveSpeedActive = speed;
    }

    void Update()
    {
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

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
        HandleDebuffs();
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 dir)
    {
        if (isInAttackRange)
        {
            float shootSpeed = (float)0.1 * moveSpeedActive;
            rb.MovePosition((Vector2)transform.position + (dir * shootSpeed * Time.deltaTime));
            Shoot();
        }
        else
        {
            rb.MovePosition((Vector2)transform.position + (dir * moveSpeedActive * Time.deltaTime));
        }
    }

    public void ContactDamage(int targetHealth)
    {
        targetHealth = targetHealth - 1;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        //Vector2 transform = new Vector2(rb.x, rb.y);

        if (health <= 0)
        {
            Instantiate(xp, rb.transform.position, rb.transform.rotation);
            Destroy(gameObject);
        }
    }

    public void AddDebuff(Debuff debuff)
    {
        if (debuff.GetType() == typeof(SilverBoltDebuff))
        {
            debuffs.Add(debuff);
        }
        else
        {
            if (!debuffs.Exists(x => x.GetType() == debuff.GetType()))
            {
                debuffs.Add(debuff);
            }
        }
    }

    private void HandleDebuffs()
    {
        //reverse iterating, finding counting and deleting silverbolt stacks
        foreach (Debuff item in debuffs.Reverse<Debuff>())
        {
            if (item.GetType() == typeof(SilverBoltDebuff))
            {
                silverBoltStacks++;
                debuffs.Remove(item);
            }
        }

        if (silverBoltStacks == 3)
        {
            TakeDamage(maxHealth * (float)0.12);
            silverBoltStacks = 0;
        }

        foreach (Debuff debuff in debuffs)
        {
            debuff.Update();
        }
    }

    public void Shoot()
    {
        Vector2 playerPosition = player.transform.position;
        playerPosition.Normalize();

        if (Time.time > lastShootTime + fireRate)
        {

            GameObject bullet = Instantiate(bulletPrefab, rb.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

            rbBullet.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);

            lastShootTime = Time.time;
        }
    }
}

