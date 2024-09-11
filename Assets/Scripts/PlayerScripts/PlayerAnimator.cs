using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// handles player animations and sprite changes
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    PlayerController playerController;
    WeaponController weaponController; 

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponent<PlayerStats>().weaponController;
    }

    // Update is called once per frame
    void Update()
    {
        
        SpriteFacing();
        
        animator.SetBool("Move", playerController.IsMoving());
    }


    /// <summary>
    /// which way the player is facing
    /// </summary>
    void SpriteFacing()
    {
        //if we don't have an enemy nearby, it will face wherever the joysticks moves
        if (weaponController.closestEnemy == null)
        {
            if (playerController.IsMoving()) 
            {
                if (transform.localScale.x > 0 && !playerController.MovingRight())
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.localScale.x < 0 && playerController.MovingRight())
                {
                    transform.localScale = Vector3.one;
                }
            }
            
        }
        else //if we have an enemy it will face the enemy in order to shoot
        {
            if (weaponController.closestEnemy.position.x >= transform.position.x)
            {
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
       
    }
}
