using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne_Shooting : MonoBehaviour, IShooting
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject condemnPrefab;
    public GameObject aim;

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

    [HideInInspector]
    public bool tumbleModifierShoot = false;

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
        switch (numberOfProjectiles)
        {
            case 1:
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<BulletBehavior>().damage = damage;
                bullet.GetComponent<BulletBehavior>().isSilverBolt = true;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                //check if the next auto should be modified by tumble damage modifier
                if (tumbleModifierShoot)
                {
                    bullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                    tumbleModifierShoot = false;
                }
                rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
                break;

            case 2:
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    tempBullet.GetComponent<BulletBehavior>().damage = damage;
                    tempBullet.GetComponent<BulletBehavior>().isSilverBolt = true;
                    Rigidbody2D tempRb = tempBullet.GetComponent<Rigidbody2D>();

                    Vector2 dir = aim.transform.rotation * Vector2.right;

                    if (tumbleModifierShoot)
                    {
                        tempBullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                        tumbleModifierShoot = false;
                    }
                    switch (i)
                    {
                        case 0:
                            Vector3 pdir = Vector2.Perpendicular(dir) * (projectileSpread / 2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 1:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / -2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                    }
                }
                break;

            case 3:
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    tempBullet.GetComponent<BulletBehavior>().damage = damage;
                    tempBullet.GetComponent<BulletBehavior>().isSilverBolt = true;
                    Rigidbody2D tempRb = tempBullet.GetComponent<Rigidbody2D>();

                    Vector2 dir = aim.transform.rotation * Vector2.right;

                    if (tumbleModifierShoot)
                    {
                        tempBullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                        tumbleModifierShoot = false;
                    }
                    switch (i)
                    {
                        case 0:
                            Vector3 pdir = Vector2.Perpendicular(dir) * (projectileSpread / 2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 1:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / -2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 2:
                            //pdir = Vector2.Perpendicular(dir) * (projectileSpread / -2);
                            tempRb.AddForce((firePoint.right) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                    }
                }
                break;
            case 4:
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    tempBullet.GetComponent<BulletBehavior>().damage = damage;
                    tempBullet.GetComponent<BulletBehavior>().isSilverBolt = true;
                    Rigidbody2D tempRb = tempBullet.GetComponent<Rigidbody2D>();

                    Vector2 dir = aim.transform.rotation * Vector2.right;

                    if (tumbleModifierShoot)
                    {
                        tempBullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                        tumbleModifierShoot = false;
                    }
                    switch (i)
                    {
                        case 0:
                            Vector3 pdir = Vector2.Perpendicular(dir) * (projectileSpread / 2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 1:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / 6 );
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 2:
                            pdir = Vector2.Perpendicular(dir) * (-1 * projectileSpread / 6 );
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 3:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / -2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                    }
                }
                break;
            case 5:
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    tempBullet.GetComponent<BulletBehavior>().damage = damage;
                    tempBullet.GetComponent<BulletBehavior>().isSilverBolt = true;
                    Rigidbody2D tempRb = tempBullet.GetComponent<Rigidbody2D>();

                    Vector2 dir = aim.transform.rotation * Vector2.right;

                    if (tumbleModifierShoot)
                    {
                        tempBullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                        tumbleModifierShoot = false;
                    }
                    switch (i)
                    {
                        case 0:
                            Vector3 pdir = Vector2.Perpendicular(dir) * (projectileSpread / 2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 1:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / 4);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 2:
                            //pdir = Vector2.Perpendicular(dir) * ((-1 * projectileSpread / 3) / 2);
                            tempRb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 3:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / -4);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 4:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / -2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                    }
                }
                break;
            case 6:
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    tempBullet.GetComponent<BulletBehavior>().damage = damage;
                    tempBullet.GetComponent<BulletBehavior>().isSilverBolt = true;
                    Rigidbody2D tempRb = tempBullet.GetComponent<Rigidbody2D>();

                    Vector2 dir = aim.transform.rotation * Vector2.right;

                    if (tumbleModifierShoot)
                    {
                        tempBullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                        tumbleModifierShoot = false;
                    }
                    switch (i)
                    {
                        case 0:
                            Vector3 pdir = Vector2.Perpendicular(dir) * (projectileSpread / 2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 1:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / 10);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 2:
                            pdir = Vector2.Perpendicular(dir) * (3* projectileSpread / 10);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 3:
                            pdir = Vector2.Perpendicular(dir) * (3*projectileSpread / -10);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 4:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / -10);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                        case 5:
                            pdir = Vector2.Perpendicular(dir) * (projectileSpread / -2);
                            tempRb.AddForce((firePoint.right + pdir) * bulletSpeed, ForceMode2D.Impulse);
                            break;
                    }
                }
                break;
            default:
                GameObject defaultbullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                defaultbullet.GetComponent<BulletBehavior>().damage = damage;
                defaultbullet.GetComponent<BulletBehavior>().isSilverBolt = true;
                Rigidbody2D defaultrb = defaultbullet.GetComponent<Rigidbody2D>();

                //check if the next auto should be modified by tumble damage modifier
                if (tumbleModifierShoot)
                {
                    defaultbullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
                    tumbleModifierShoot = false;
                }
                defaultrb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);

                defaultrb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
                break;
        }


        //rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    



        lastShootTime = Time.time;
    }

    
    public void Condemn()
    {
        GameObject condemn = Instantiate(condemnPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = condemn.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }

}
