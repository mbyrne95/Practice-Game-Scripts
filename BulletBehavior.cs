using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //public GameObject hitEffect;
    public float damage;
    private float condemnStrength; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Default"));
        }

        if (collision.gameObject.TryGetComponent<BasicEnemyAI>(out BasicEnemyAI enemy))
        {
            enemy.TakeDamage(damage);
            
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>() != null)
            {

                condemnStrength = GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().knockBackStrength;

                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().condemnModifierBool)
                {
                    enemy.rb.AddForce((collision.transform.position - transform.position) * condemnStrength, ForceMode2D.Force);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Vayne_Shooting>().condemnModifierBool = false;
                }
            }
            
        }       
        Destroy(gameObject);
    }
}
