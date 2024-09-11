using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.GraphicsBuffer;

public class WeaponController : MonoBehaviour
{
    //references
    private Joystick joystick;
    private UIManager uiManager; 

    //statistics
    [SerializeField]
    private Transform projectileSpawnPoint; //point to spawn the bullet
    [SerializeField]
    private WeaponSO currentWeapon; //this weapon's stats
    private int currentNumberOfProjectiles; //the max number of bullets
    public int CurrentNumberOfProjectiles
    {
        get => currentNumberOfProjectiles; private set => currentNumberOfProjectiles = value;
    }
    public float weaponCurrentCooldown; //the cooldown delay between shots
    private float weaponCooldownAfterFireRatePowerUp; //a float to keep track of the current cooldown if affacted by the fire rate power up
    private float currentWeaponDamage; //the current damage the weapon does
    public float CurrentWeaponDamage
    {
        get => currentWeaponDamage; private set => currentWeaponDamage = value;
    }
    [HideInInspector]
    public StatusEffectSO currentStatusEffect;

    //enemies
    private SpriteRenderer spriteRenderer; //ref to the sprite renderer
    private List<Projectile> projectilePool  = new List<Projectile>(); //polling list
    int currentBulletIndex = 0; //To keep track of the bullet in the pool
    private CircleCollider2D physicalWeaponRadius; //the collider that detects enemies in range
    private List<Transform> enemiesInRange= new List<Transform>(); //the enemies in range
    public Transform closestEnemy; //the closest target to shoot
    private float shortestDistanceToEnemy = 0f; //keeps track of the distance to the closes enemy

    /// <summary>
    /// inject references to Joystick and UIManager
    /// </summary>
    /// <param name="newJoystick"></param>
    /// <param name="newUIManager"></param>
    [Inject]
    public void Construct(Joystick newJoystick, UIManager newUIManager)
    {
        this.joystick = newJoystick;
        this.uiManager = newUIManager;

    }

    private void Awake()
    {
        physicalWeaponRadius = GetComponent<CircleCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        InitializeWeapon();
    }


    // Update is called once per frame
    void Update()
    {
        CheckForClosestEnemy();
        //rotation
        if (closestEnemy == null)
        {
            float angle = MovingRight() ? Mathf.Atan2(joystick.joystickVec.y, joystick.joystickVec.x) * Mathf.Rad2Deg : Mathf.Atan2((-1) * joystick.joystickVec.y, (-1) * joystick.joystickVec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector3 targetDirection = closestEnemy.position - transform.position;

            float angle = closestEnemy.position.x >= transform.position.x? Mathf.Atan2(targetDirection.y,  targetDirection.x) * Mathf.Rad2Deg: Mathf.Atan2((-1) * targetDirection.y, (-1) * targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //weapon cooldown
            weaponCurrentCooldown -= Time.deltaTime;
            if (weaponCurrentCooldown <= 0f && currentNumberOfProjectiles>0)
            {
                Attack();
            }

        }

        
    }

    /// <summary>
    /// called at the beginning to set up the weapon and store temporary variables that can be modified at runtime
    /// </summary>
    private void InitializeWeapon()
    {
        currentNumberOfProjectiles = currentWeapon.RemainingBullets;
        weaponCurrentCooldown = weaponCooldownAfterFireRatePowerUp = currentWeapon.WeaponCooldown;
        spriteRenderer.sprite = currentWeapon.WeaponSprite;
        currentWeaponDamage = currentWeapon.WeaponDamage;
        physicalWeaponRadius.radius = shortestDistanceToEnemy = currentWeapon.WeaponRange; //set the radius to detect enemies
        for (int i = 0; i < 40; i++) //40 is a temporary number
        {
            Projectile newProjectile = Instantiate(currentWeapon.WeaponProjectile, projectileSpawnPoint.transform).GetComponent<Projectile>();
            newProjectile.InitializeProjectile(this, currentWeaponDamage, currentWeapon.WeaponProjectileSpeed);
            projectilePool.Add(newProjectile);
        }
   
    }

    /// <summary>
    /// shoots a projectile
    /// </summary>
    private void Attack()
    {
        weaponCurrentCooldown = weaponCooldownAfterFireRatePowerUp;
        //keep track of projectiles
        currentNumberOfProjectiles--;
        if(currentNumberOfProjectiles < 0){
            currentNumberOfProjectiles = 0;
            uiManager.UpdateBulletCount(currentNumberOfProjectiles);
            return;
        }
        uiManager.UpdateBulletCount(currentNumberOfProjectiles); //update UI with the bullet change
        //iterate through the projectile pool
        currentBulletIndex++;
        if (currentBulletIndex > projectilePool.Count - 1)
        {
            currentBulletIndex = 0;
        }
        projectilePool[currentBulletIndex].gameObject.SetActive(true);
    }

    /// <summary>
    /// called upon picking up the Ammo Box Item. Increases projectiles
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseNumberOfProjectiles(int amount)
    {
        currentNumberOfProjectiles += amount;
        uiManager.UpdateBulletCount(currentNumberOfProjectiles); //update UI with the bullet change
    }
    /// <summary>
    /// resets projectile at the start after it hits a target or it disables itself
    /// </summary>
    /// <param name="projectile"></param>
    public void ResetProjectile(GameObject projectile)
    {
        projectile.transform.SetParent(projectileSpawnPoint.transform, false);
        projectile.transform.position = projectileSpawnPoint.position;
        projectile.SetActive(false);
    }

    /// <summary>
    /// a boolean to determine what direction the player is facing
    /// </summary>
    /// <returns></returns>
    public bool MovingRight()
    {
        return joystick.joystickVec.x >= 0;
    }


    /// <summary>
    /// gets the closest enemy in range to shoot at
    /// </summary>
    private void CheckForClosestEnemy()
    {
        foreach (Transform potentialTarget in enemiesInRange)
        {
            Vector3 directionToTarget = potentialTarget.position - transform.position;
            float distToTarget = directionToTarget.magnitude;
            if (distToTarget < shortestDistanceToEnemy)
            {
                shortestDistanceToEnemy = distToTarget;
                closestEnemy = potentialTarget;
            }
        }
    }


    /// <summary>
    /// applies the power up to the weapon
    /// </summary>
    /// <param name="powerUp"></param>
    public void ApplyPowerUp(PowerUpSO powerUp)
    {

        switch (powerUp.TypeOfPowerUp)
        {
            case PowerUpSO.PowerUpType.MaxHealth:
                break;
            case PowerUpSO.PowerUpType.Speed:
                break;
            case PowerUpSO.PowerUpType.Damage:
                currentWeaponDamage += powerUp.Amount;
                
                break;
            case PowerUpSO.PowerUpType.FireRate:
                weaponCooldownAfterFireRatePowerUp -= powerUp.Amount; //decreases the cooldown between shots
                if (weaponCooldownAfterFireRatePowerUp <= 0)
                {
                    weaponCooldownAfterFireRatePowerUp = 0.1f;
                }
               
                break;
            case PowerUpSO.PowerUpType.PoisonDamage:
                if (powerUp.StatusEffect != null)
                {
                    currentStatusEffect = powerUp.StatusEffect;
                }

                break;
            default:
                break;
        }

    }

    #region KeepTrackOfEnemies

    /// <summary>
    /// on triggerenter and ontriggerexit to mark enemies within range
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            if (!enemiesInRange.Contains(collision.transform))
            {
                enemiesInRange.Add(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            if (enemiesInRange.Contains(collision.transform))
            {
                enemiesInRange.Remove(collision.transform);
                closestEnemy = null;
                shortestDistanceToEnemy = currentWeapon.WeaponRange;
            }
        }
    }

    #endregion

}
