using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/AttackDamageBuff")]
public class AttackDamageBuff : PowerupEffect
{
    public float attackDamageBuff;
    public float moveSpeedPenalty;

    public override void Apply(GameObject target)
    {
        IShooting targetShooting = target.GetComponent<IShooting>();
        PlayerMovement playerMove = target.GetComponent<PlayerMovement>();
        float currentAD = targetShooting.damage;
        float currentMS = playerMove.moveSpeedBase;

        playerMove.moveSpeedBase = (1 - moveSpeedPenalty) * currentMS;
        targetShooting.damage = (1 + attackDamageBuff) * currentAD;
    }

}
