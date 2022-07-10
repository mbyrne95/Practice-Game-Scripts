using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/AttackspeedBuff")]
public class AttackSpeedBuff : PowerupEffect
{

    public float attackSpeedPercent;
    public float attackDamagePenalty;

    public override void Apply(GameObject target)
    {
        IShooting targetShooting = target.GetComponent<IShooting>();
        float currentAS = targetShooting.fireRate;
        float currentAD = targetShooting.damage;

        targetShooting.fireRate = (1 - attackSpeedPercent) * currentAS;

        targetShooting.damage = (1 - attackDamagePenalty) * currentAD;
    }
}
