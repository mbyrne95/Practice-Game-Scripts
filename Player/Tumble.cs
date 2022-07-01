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

        //Transform currentTransform = parent.transform;
        //Vector2 moveDirection = movement.moveDirection;
            //Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        movement.moveSpeedActive = movement.moveSpeedActive * dashVelocity;

        parent.GetComponent<Vayne_Shooting>().tumbleModifierBool = true;
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.moveSpeedActive = movement.moveSpeedBase;
    }
}