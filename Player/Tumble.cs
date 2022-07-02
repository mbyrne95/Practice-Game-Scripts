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
        //movement.state = PlayerMovement.movementState.misc;
        movement.tumbleModifierBool = true;
        parent.GetComponent<Vayne_Shooting>().tumbleModifierShoot = true;

        //movement.moveSpeedActive = movement.moveSpeedBase * dashVelocity;

        //parent.GetComponent<Vayne_Shooting>().tumbleModifierBool = true;
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.tumbleModifierBool = false;
        //parent.GetComponent<Vayne_Shooting>().tumbleModifierShoot = false;

        //movement.state = PlayerMovement.movementState.normal;
        //movement.moveSpeedActive = movement.moveSpeedBase;
    }
}