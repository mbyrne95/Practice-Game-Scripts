using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpShard : MonoBehaviour, ICollectible
{
    //public static event Action OnXpCollected;
    public float xpAmount; 
    
    public void Collect()
    {
        //OnXpCollected?.Invoke();
        GameObject.FindGameObjectWithTag("Player").GetComponent<LevelSystem>().GainExperienceFlatRate(xpAmount);
        Destroy(gameObject);
    }
}
