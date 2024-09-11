using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Zenject;

public class PlayerStats : MonoBehaviour
{

    public PlayerSO playerData;//reference to the scriptable object

    //current stats. We use these in case the player gets a buff so that they can be modified temporarily
    float currentHealth;
    float currentMaxHealth;
    float currentMoveSpeed;
    public float CurrentMoveSpeed
    {
        get => currentMoveSpeed; private set => currentMoveSpeed = value;
    }
    float currentInvincibiltyDuration;



    //experience and level of the player
    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    public int enemiesKilled = 0;

    //Invincibilty logic. used so that the player doesn't take constant damage, but only on an interval
    private float invincibilityTimer; //for how long the player is invincible
    private bool isInvincible; //flag to know when player is invincible
   
    /// <summary>
    /// Level up Logic. the levels are split into groups(e.g. between level 1 and 4 the exp cap is 100, from 5 to 10 it is 200 etc)
    /// </summary>
    [System.Serializable]
    public class LevelGroup
    {
        public int startLevel;
        public int endLevel;
        public int experianceCapIncrease; //by how much the experience cap increases from level to level
    }
    public List<LevelGroup> levelGroups = new List<LevelGroup>();


    //Game Management
    GameManager gameManager;
    private UIManager uiManager;
    public WeaponController weaponController;

    void Awake()
    {
        currentHealth = currentMaxHealth = playerData.MaxHealth;
        currentMoveSpeed = playerData.MoveSpeed;
        currentInvincibiltyDuration = playerData.InvincibilityDuration;
    }

    private void Start()
    {
        //initialize the experience cap 
        experienceCap = levelGroups[0].experianceCapIncrease;
    }

    /// <summary>
    /// inject references to GameManager and UIManager
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="newUIManager"></param>
    [Inject]
    public void Construct(GameManager gameManager, UIManager newUIManager)
    {
        this.gameManager = gameManager;
        this.uiManager = newUIManager;

    }

    private void Update()
    {
        //handle invincibility
        if (isInvincible) 
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        } 
    }
    /// <summary>
    /// increases experience
    /// </summary>
    /// <param name="exp"></param>
    public void IncreaseExperience(int exp)
    {
        experience += exp;
       
        CheckForLevelUp();
    }


    /// <summary>
    /// restores health on pick up
    /// </summary>
    /// <param name="health"></param>
    public void RestoreHealth(int health)
    {
     
        currentHealth += health;
        if (currentHealth > currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
        uiManager.UpdateHealthBar(currentHealth/currentMaxHealth);
    }

    /// <summary>
    /// keep track of enemy kills
    /// </summary>
    public void RecordEnemyKill()
    {
        enemiesKilled++;
        uiManager.UpdateKillCount(enemiesKilled);
    }

    /// <summary>
    /// checkes the gathered experience and sees if it's time to level up
    /// </summary>
    private void CheckForLevelUp()
    {
        if (experience >= experienceCap)
        {
            level++;
            uiManager.UpdateLevel(level);
            experience -= experienceCap; //reset the experience
            foreach (LevelGroup levelGroup in levelGroups) 
            {   
                if (level >=levelGroup.startLevel && level <= levelGroup.endLevel)
                {
                    experienceCap += levelGroup.experianceCapIncrease; //increase the current exp cap to make it harder to level up to the next level
                    break;
                }
               
            }
            gameManager.LevelUp();
        }
        float expRatio = (float)experience/(float) experienceCap;
        uiManager.UpdateExpBar(expRatio);
    }

    /// <summary>
    /// take damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            uiManager.UpdateHealthBar(currentHealth/currentMaxHealth);
            Die();
            return;
        }
        uiManager.UpdateHealthBar(currentHealth / currentMaxHealth);
        invincibilityTimer = currentInvincibiltyDuration;
        isInvincible = true;
        
    }


    /// <summary>
    /// called when the player dies
    /// </summary>
    public void Die()
    {
        if (!gameManager.isGameOver)
        {
            gameManager.GameOver();
        }
    }

    /// <summary>
    /// applies the power up to the player
    /// </summary>
    /// <param name="powerUp"></param>
    public void ApplyPowerUp(PowerUpSO powerUp) 
    {

        switch (powerUp.TypeOfPowerUp)
        {
            case PowerUpSO.PowerUpType.MaxHealth:
                currentMaxHealth += powerUp.Amount;
                uiManager.UpdateHealthBar(currentHealth / currentMaxHealth);
               
                
                break;
            case PowerUpSO.PowerUpType.Speed:
                currentMoveSpeed += powerUp.Amount;
                
                break;
            case PowerUpSO.PowerUpType.Damage:

                break;
            case PowerUpSO.PowerUpType.FireRate:
                break;
            default:
                break;
        }
        
    }



}
