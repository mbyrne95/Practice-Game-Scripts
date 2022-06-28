using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //public GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Default"));
        }

        if (collision.gameObject.TryGetComponent<BasicEnemyAI>(out BasicEnemyAI enemy))
        {
            enemy.TakeDamage(GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().damage);
        }
        
        Destroy(gameObject);
    }
}
