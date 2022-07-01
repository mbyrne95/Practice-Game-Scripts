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

    public bool tumbleModifierBool = false;

    float IShooting.fireRate { get => fireRate; set => fireRate = value; }
    float IShooting.damage { get => damage; set => damage = value; }

    // Update is called once per frame

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastShootTime + fireRate)
        {
            //GetComponentInParent<PlayerMovement>().isCharacterFiring = true;
            Shoot();
            lastShootTime = Time.time;
        }
        //GetComponentInParent<PlayerMovement>().isCharacterFiring = false;
    }

    public void Shoot()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Default"));
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletBehavior>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //check if the next auto should be modified by tumble damage modifier
        if (tumbleModifierBool)
        {
            bullet.GetComponent<BulletBehavior>().damage = tumbleMultiplier * damage;
            tumbleModifierBool = false;
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
