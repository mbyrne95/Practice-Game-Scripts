using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //public GameObject hitEffect;
    public float damage;

    [HideInInspector]
    public bool isSilverBolt = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IEnemy>(out IEnemy enemy))
        {
            if (isSilverBolt)
            {
                enemy.AddDebuff(new SilverBoltDebuff(enemy));
            }
            enemy.TakeDamage(damage);
        }               
        Destroy(gameObject);
    }
}
