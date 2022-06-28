using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts; //this is effectively max health

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Collision col;

    [SerializeField]
    private float iFrames;

    private bool isInvincible = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            } 
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }
        health -= damage;
        StartCoroutine(BecomeTemporarilyInvincible());
    }

    //can't be damaged during this time + temporarily ignore unit collision
    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(GameObject.FindGameObjectWithTag("Enemy").layer, gameObject.layer);

        yield return new WaitForSeconds(iFrames);

        Physics2D.IgnoreLayerCollision(GameObject.FindGameObjectWithTag("Enemy").layer, gameObject.layer, false);
        isInvincible = false;
    }
}
