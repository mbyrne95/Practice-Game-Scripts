using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Blink : Ability
{
    public float blinkDistance;
    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();

        Transform currentTransform = parent.transform;
        Vector2 moveDirection = movement.moveDirection;

        if (moveDirection.x < 0)
        {
            if (moveDirection.y < 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x + (-1 * blinkDistance), currentTransform.position.y + (-1 * blinkDistance), 0);
            }
            else if (moveDirection.y > 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x + (-1 * blinkDistance), currentTransform.position.y + blinkDistance, 0);
            }
            else if (moveDirection.y == 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x + (-1 * blinkDistance), currentTransform.position.y, 0);
            }

        }
        else if (moveDirection.x > 0)
        {
            if (moveDirection.y < 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x + blinkDistance, currentTransform.position.y + (-1 * blinkDistance), 0);
            }
            else if (moveDirection.y > 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x + blinkDistance, currentTransform.position.y + blinkDistance, 0);
            }
            else if (moveDirection.y == 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x + blinkDistance, currentTransform.position.y, 0);
            }
        }
        else if (moveDirection.x == 0)
        {
            if (moveDirection.y < 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x, currentTransform.position.y + (-1 * blinkDistance), 0);
            }
            else if (moveDirection.y > 0)
            {
                parent.transform.position = new Vector3(currentTransform.position.x, currentTransform.position.y + blinkDistance, 0);
            }

            //Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
            //movement.moveSpeedActive = movement.moveSpeedActive * dashVelocity;
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        //movement.moveSpeedActive = movement.moveSpeedBase;
    }
}
