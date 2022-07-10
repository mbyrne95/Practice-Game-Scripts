using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Condemn : Ability
{
    //public float knockBackStrength;

    public override void Activate(GameObject parent)
    {
        parent.GetComponent<Vayne_Shooting>().Condemn();
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
