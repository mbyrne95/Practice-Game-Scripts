using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff 
{
    protected IEnemy enemy;

    public Debuff(IEnemy enemy)
    {
        this.enemy = enemy;
    }

    public virtual void Update()
    {

    }
}
