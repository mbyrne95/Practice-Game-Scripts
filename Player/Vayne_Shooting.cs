using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne_Shooting : MonoBehaviour, IShooting
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject condemnPrefab;
    
    public float bulletSpeed = 20f;

    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float damage;

    public float tumbleMultiplier;
    public float condemnDamage;

    private float lastShootTime = 0;

    [HideInInspector]
    public bool tumbleModifierShoot = false;

    float IShooting.fireRate { get => fireRate; set => fireRate = value; }
    float IShooting.damage { get => damage; set => damage = value; }

    // Update is called once per frame

    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Default"));
    }


    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastShootTime + fireRate)
        {
            Shoot();
            lastShootTime = Time.time;
            //GetComponentInParent<PlayerMovement>().state = PlayerMovement.movementState.shooting;
        }
        /*
        else
        {
            GetComponentInParent<PlayerMovement>().state = PlayerMovement.movementState.normal;
        }
        */
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletBehavior>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //check if the next auto should be modified by tumble damage modifier
        if (tumbleModifierShoot)
        {
            bullet.GetComponent<BulletBehavior>().damage *= tumbleMultiplier;
            tumbleModifierShoot = false;
        }

        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);

        lastShootTime = Time.time;
    }

    
    public void Condemn()
    {
        GameObject condemn = Instantiate(condemnPrefab, firePoint.position, firePoint.rotation);
        //condemn.GetComponent<CondemnBehavior>().damage = condemnDamage;
        Rigidbody2D rb = condemn.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
    }

}
