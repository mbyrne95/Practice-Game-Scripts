using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondemnBehavior : MonoBehaviour
{
    [SerializeField]
    private float condemnStrength;

    private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IEnemy>(out IEnemy enemy))
        {
            damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().condemnDamage;
            enemy.TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * condemnStrength, ForceMode2D.Force);
        }
        Destroy(gameObject);
    }
}
