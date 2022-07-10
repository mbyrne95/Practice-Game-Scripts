using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/SlowingAutoBuff")]
public class SlowingAutoBuff : PowerupEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<IShooting>().slowDebuff = true;
    }
}
