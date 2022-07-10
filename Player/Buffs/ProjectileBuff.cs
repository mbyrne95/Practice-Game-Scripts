using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ProjectileBuff")]
public class ProjectileBuff : PowerupEffect
{
    public int projectileUp = 1;
    public float attackDamagePenalty;
    public float attackSpeedBuff;

    public override void Apply(GameObject target)
    {
        IShooting targetShooting = target.GetComponent<IShooting>();
        float currentAS = targetShooting.fireRate;
        float currentAD = targetShooting.damage;
        int currentNumProjectiles = targetShooting.numProjectiles;

        //this will be checked in buff menu code as well - double checking here just to be sure we don't crash
        if (currentNumProjectiles < 6)
        {
            targetShooting.numProjectiles = currentNumProjectiles + projectileUp;
        }

        targetShooting.fireRate = (1 - attackSpeedBuff) * currentAS;
        targetShooting.damage = (1 - attackDamagePenalty) * currentAD;
    }
}