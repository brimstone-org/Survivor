using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.Asteroids;

public class Projectile : MonoBehaviour
{
    private Vector3 direction; //direction the projectile is going in
    [SerializeField]
    private float destroyAfterSeconds; //the projectileis reset if it goes into space after these seconds
    private float speed; //move speed
    private float damage; //the damage the projectile does
    private WeaponController weaponController; //reference to the weapon firing it
    private Coroutine lifespanCoroutine; //a coroutine to keep track of the self destruct timer
    private StatusEffectSO currentStatusEffect; //the potential status effect of the bullet

    private void Update()
    {
        transform.position += direction * speed *Time.deltaTime;
    }

    /// <summary>
    /// called when the object is instantiated from the WeaponController.cs
    /// </summary>
    /// <param name="newController"></param>
    /// <param name="newDamage"></param>
    /// <param name="newSpeed"></param>
    public void InitializeProjectile(WeaponController newController, float newDamage, float newSpeed )
    {
        weaponController = newController;
        damage = newDamage;
        speed = newSpeed;
        gameObject.SetActive( false );
    }

    /// <summary>
    /// quick check for stats when it it reenabled from the pool
    /// </summary>
    private void OnEnable()
    {
        if (weaponController !=null)
        {
            direction = weaponController.closestEnemy.transform.position.x >= weaponController.transform.position.x? weaponController.transform.right  : weaponController.transform.right * (-1);
            lifespanCoroutine = StartCoroutine(DisableWithDelay());
            transform.parent = null;

            if (weaponController.CurrentWeaponDamage != damage)
            {
                damage = weaponController.CurrentWeaponDamage;
            }
            if (weaponController.currentStatusEffect!=null && weaponController.currentStatusEffect != currentStatusEffect) //if the weapon status effect has changed
            {
                currentStatusEffect = weaponController.currentStatusEffect;
            }
        }
        
    }

    private void OnDisable()
    {
        if (lifespanCoroutine != null)
        {
            StopCoroutine(lifespanCoroutine);
        }
    }


    /// <summary>
    /// coroutine to disable the bullet if it hits nothing
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableWithDelay() 
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / destroyAfterSeconds;
            yield return null;
        }
        weaponController.ResetProjectile(gameObject);
    }

    /// <summary>
    /// do enemy damage logic
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(damage, currentStatusEffect);
            if (lifespanCoroutine != null)
            {
                StopCoroutine(lifespanCoroutine);
            }
            weaponController.ResetProjectile(gameObject);
        }
    }
}
