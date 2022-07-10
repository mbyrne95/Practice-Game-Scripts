using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Tumble : Ability
{
    public float dashVelocity;
    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.tumbleModifierBool = true;
        parent.GetComponent<Vayne_Shooting>().tumbleModifierShoot = true;
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.tumbleModifierBool = false;

        // turning off the Shoot modifier bool is handled in the shooting script
    }
}