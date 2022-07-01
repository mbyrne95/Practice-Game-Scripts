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
        if (collision.gameObject.tag == "Player")
        {
        }

        if (collision.gameObject.TryGetComponent<BasicEnemyAI>(out BasicEnemyAI enemy))
        {
            damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().condemnDamage;
            enemy.TakeDamage(damage);
            enemy.rb.AddForce((collision.transform.position - transform.position) * condemnStrength, ForceMode2D.Force);

            /*
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>() != null)
            {
                damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().condemnDamage;
                condemnStrength = GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().knockBackStrength;

                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().condemnModifierBool)
                {
                    
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().condemnModifierBool = false;
                }
            }
            */

        }
        Destroy(gameObject);
    }
}
