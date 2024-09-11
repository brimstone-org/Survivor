using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject]
    private Joystick joystick; 
    public PlayerStats playerStats;
    Rigidbody2D rb2D; //rigidbody reference
    private Vector2 movementDir; //direction in which the player is moving

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }


    private void Update()
    {
        movementDir = new Vector2(joystick.joystickVec.x, joystick.joystickVec.y).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }



    /// <summary>
    /// moves the player
    /// </summary>
    private void Move()
    {
        if (joystick.IsBeingtouched())
        {
            rb2D.velocity = new Vector2(movementDir.x * playerStats.CurrentMoveSpeed, movementDir.y * playerStats.CurrentMoveSpeed);
        }
        else
        {
            if (rb2D.velocity.x != 0f || rb2D.velocity.y !=0f) 
            {
                rb2D.velocity = Vector2.zero;
            }
            
        }
    }


    public bool IsMoving()
    {
        return (movementDir.x != 0 || movementDir.y != 0);
    }

    //check direction of movement
    public bool MovingRight()
    {
        return movementDir.x >= 0;
    }
}
