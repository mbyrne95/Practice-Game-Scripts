using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vayne_Shooting : MonoBehaviour, IShooting
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    public float bulletSpeed = 20f;

    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float damage;

    private float currentDamage;

    private float lastShootTime = 0;

    float IShooting.fireRate { get => fireRate; set => fireRate = value; }
    float IShooting.damage { get => damage; set => damage = value; }

    // Update is called once per frame

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastShootTime + fireRate)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    public void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);

        lastShootTime = Time.time;

    }

    public IEnumerator DashModifier()
    {
        currentDamage = damage;
        damage = (float)1.6 * damage;
        yield return new WaitForSeconds(1);
        damage = currentDamage;
    }
}