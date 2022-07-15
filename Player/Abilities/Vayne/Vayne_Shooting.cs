using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne_Shooting : MonoBehaviour, IShooting
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject condemnPrefab;
    public GameObject aim;

    [HideInInspector]
    public List<int> numbers = new List<int>();

    [Range(1,6)]
    public int numberOfProjectiles = 1;
    [SerializeField]
    private float projectileSpread = (float)0.5;
    
    public float bulletSpeed = 10f;

    [SerializeField]
    private float fireRate;
    [SerializeField]
    public float damage;

    public float tumbleMultiplier;
    public float condemnDamage;

    private float lastShootTime = 0;

    
    public bool slowDebuff = false;

    [HideInInspector]
    public bool tumbleModifierShoot = false;

    //List<int> IShooting.numbers { get => numbers; set => numbers = value; }
    bool IShooting.slowDebuff { get => slowDebuff; set => slowDebuff = value; }
    int IShooting.numProjectiles { get => numberOfProjectiles; set => numberOfProjectiles = value; }
    float IShooting.fireRate { get => fireRate; set => fireRate = value; }
    float IShooting.damage { get => damage; set => damage = value; }

    //private List<Powerup> powerups = new List<Powerup>();

    // Update is called once per frame

    void Start()
    {
        //ignore all collision between default layer and player layer - all friendly projectiles should be in default
        //xp and pickups should additionally be in player
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PlayerBullets"));
    }


    public void Update()
    {
        //HandlePowerUps();
        if (Input.GetButton("Fire1") && Time.time > lastShootTime + fireRate)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    public void Shoot()
    {
        List<int> denominator = new List<int>();
        List<int> numerator = new List<int>();

        switch (numberOfProjectiles)
        {
            case 1:
                numerator.Add(0);
                denominator.Add(1);
                break;
            case 2:
                numerator.AddRange(new List<int> { 1, 1 });
                denominator.AddRange(new List<int> { 2, -2 });
                break;
            case 3:
                numerator.AddRange(new List<int> { 1, 0, 1 });
                denominator.AddRange(new List<int> { 2, 1, -2 });
                break;
            case 4:
                numerator.AddRange(new List<int> { 1, 1, 1, 1 });
                denominator.AddRange(new List<int> { 2, 6, -6, -2 });
                break;
            case 5:
                numerator.AddRange(new List<int> { 1, 1, 0, 1, 1 });
                denominator.AddRange(new List<int> { 2, 4, 1, -4, -2 });
                break;
            case 6:
                numerator.AddRange(new List<int> { 1, 1, 3, 3, 1, 1 });
                denominator.AddRange(new List<int> { 2, 10, 10, -10, -10, -2 });
                break;
            default:
                numerator.Add(0);
                denominator.Add(1);
                break;
        }

        if (denominator.Count != numberOfProjectiles)
        {
            throw new System.Exception("this is broken");
        }

        for(int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            tempBullet.GetComponent<BulletBehavior>().damage = damage;
            tempBullet.GetComponent<BulletBehavior>().isSilverBolt = true;

            if (slowDebuff)
            {
                tempBullet.GetComponent<BulletBehavior>().slowDebuff = true;
            }

            Rigidbody2D tempRb = tempBullet.GetComponent<Rigidbody2D>();

            Vector2 dir = aim.transform.rotation * Vector2.right;

            if (tumbleModifierShoot)
            {
                tempBullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                tumbleModifierShoot = false;
            }

            Vector3 pdir = Vector2.Perpendicular(dir) * ((numerator[i] * projectileSpread) / denominator[i]);
            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
        }
        lastShootTime = Time.time;
    }

    
    public void Condemn()
    {
        GameObject condemn = Instantiate(condemnPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = condemn.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }

}
