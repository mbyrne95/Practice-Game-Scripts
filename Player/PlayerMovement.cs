using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Tumble tumble;

    [HideInInspector]
    public Vector2 moveDirection;

    //this is the final number that gets realized
    public float moveSpeedActive;

    //this is a reference, used to default back to when the moveSpeedActive is changed outside of this script
    public float moveSpeedBase;

    [HideInInspector]
    public enum movementState
    {
        normal,
        shooting,
        tumbling
    }

    [HideInInspector]
    public movementState state = movementState.normal;

    public float moveSpeedDebuffModifier;
    private float moveSpeedDebuff;

    [HideInInspector]
    public bool tumbleModifierBool = false;

    void Start()
    {
        moveSpeedActive = moveSpeedBase;
        moveSpeedDebuff = moveSpeedBase * moveSpeedDebuffModifier;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }
    
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        //check if the player is firing/dashing. Dashing always takes precedence, followed by firing.
        if(Input.GetButton("Fire1") && tumbleModifierBool)
        {
            state = movementState.tumbling;
        } 
        else if (!Input.GetButton("Fire1") && tumbleModifierBool)
        {
            state = movementState.tumbling;
        }
        else if (Input.GetButton("Fire1") && !tumbleModifierBool)
        {
            state = movementState.shooting;
        } 
        else
        {
            state = movementState.normal;
        }

        switch (state)
        {
            case movementState.shooting:
                moveSpeedActive = moveSpeedDebuff;
                break;
            case movementState.tumbling:
                moveSpeedActive = moveSpeedBase * tumble.dashVelocity;
                break;
            case movementState.normal:
                moveSpeedActive = moveSpeedBase;
                break;
            default:
                moveSpeedActive = moveSpeedBase;
                break;
        }
        
        rb.velocity = new Vector2((moveDirection.x * moveSpeedActive), (moveDirection.y * moveSpeedActive));
    }
}
