using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    private EnemySO enemyStats;

    //these vars indicate the current values for these stats. (useful when the player has certain buffs that affect the enemy temporarily
    float currentHealth;
    float currentMoveSpeed;
    public float CurrentMoveSpeed
    {
        get => currentMoveSpeed; private set => currentMoveSpeed = value;
    }
    float currentDamage;


    private float despawnDistance = 20f; //this is the distance at which this enemy is relocated close to the player if the player moves to far away from the enemy
    private SpriteRenderer spriteRenderer;

    //status effects
    private float currentEffectTime = 0f; //for howlong thie effect has been active
    private float lastTickTime = 0f; //the time of the last tick during the status effect lifetime
    private StatusEffectSO statusEffect; //if the enemy is currently affected by a status effect

    //references
    PlayerStats player; 
    private EnemySpawner enemySpawner;

    [SerializeField]
    private Animator explosion; //for the explosion effect

    private void Awake()
    {
        currentDamage = enemyStats.EnemyDamage;
        currentHealth = enemyStats.EnemyMaxHealth;
        currentMoveSpeed = enemyStats.EnemySpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    /// <summary>
    /// sets up enemy at the start
    /// </summary>
    /// <param name="ps"></param>
    /// <param name="es"></param>
    public void InitializeEnemy(PlayerStats ps, EnemySpawner es)
    {
        player = ps;
        enemySpawner = es;
        GetComponent<EnemyMovement>().InitializeMovement(player.transform);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
        if (statusEffect!=null && currentHealth > 0)
        {
            HandleStatusEffect();
        }
    }

       /// <summary>
       /// called if the enemy gets too far away from the player and reset them at a position closer
       /// </summary>
    void ReturnEnemy()
    {
        transform.position = enemySpawner.GetEnemySpawnPosition();
    }

    /// <summary>
    /// called when being hit by a bullet
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="effect"></param>
    public void TakeDamage(float damage, StatusEffectSO effect = null)
    {
        currentHealth -= damage;
        explosion.SetTrigger("Explode");
        if (effect !=null) //if there is an effect to apply
        {
            currentEffectTime = 0; //reset effect run time
            lastTickTime = 0; //reset effect last tick time
            statusEffect = effect;
        }
        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    /// <summary>
    /// handles whatever the status effect does. A switch case can be implemented for more status effects
    /// </summary>
    private void HandleStatusEffect()
    {
        currentEffectTime += Time.deltaTime;
        if (spriteRenderer.color != statusEffect.EffectColor)
        {
            spriteRenderer.color = statusEffect.EffectColor;
        }
        if (statusEffect.DotAmount != 0 && currentEffectTime > lastTickTime + statusEffect.TickSpeed)
        {
            
            lastTickTime = currentEffectTime;
            currentHealth += statusEffect.DotAmount;
            Debug.Log("Taking poison damage. My health is " + currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    /// <summary>
    /// called when the enemy dies
    /// </summary>
    private void Die()
    {
        if (statusEffect!=null)
        {
            statusEffect = null;
        }
        
        GetComponent<ItemDrop>().OnDeath();
        player.RecordEnemyKill(); //keep track of enemies killed
        Destroy(gameObject);
    }

    /// <summary>
    /// to give constant damage to the player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            PlayerStats player  = collision.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }


  
}
