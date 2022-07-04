using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicRangedEnemy : MonoBehaviour, IEnemy
{
    public GameObject xp;

    public float speed;
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

    private float tempSpeed;


    //private Vector3 startingPosition;

    private void Start()
    {
        //Physics2D.IgnoreLayerCollision();
        health = maxHealth;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        int targetHealth = target.GetComponent<Health>().health;
        col = GetComponent<Collider2D>();
        tempSpeed = speed * (float)0.1;
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
        /*
        if (col.gameObject.tag == "XP")
        {
            Physics2D.IgnoreCollision(xp.GetComponent<Collider2D>(), col);
        }
        */

        HandleDebuffs();


        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 dir)
    {
        if (isInAttackRange)
        {
            rb.MovePosition((Vector2)transform.position + (dir * tempSpeed * Time.deltaTime));
            Shoot();
        }
        else
        {
            rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
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
        debuffs.Add(debuff);
    }

    private void HandleDebuffs()
    {
        //Debug.Log(silverBoltStacks);

        var tempList = debuffs;
        //int count = 0;

        foreach (Debuff item in debuffs.Reverse<Debuff>())
        {
            if (item.GetType() == typeof(SilverBoltDebuff))
            {
                silverBoltStacks++;
                debuffs.Remove(item);
            }
            //count++;
        }

        if (silverBoltStacks == 3)
        {
            TakeDamage(maxHealth * (float)0.12);
            silverBoltStacks = 0;
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
