using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/AttackspeedBuff")]
public class AttackSpeedBuff : PowerupEffect
{

    public float attackSpeedPercent;

    public override void Apply(GameObject target)
    {
        float currentAS = target.GetComponent<IShooting>().fireRate;
        target.GetComponent<IShooting>().fireRate = currentAS - (currentAS * attackSpeedPercent);
    }
}
