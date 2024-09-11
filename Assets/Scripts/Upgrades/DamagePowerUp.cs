using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DamagePowerUp", menuName = "ScriptableObjects/PowerUps/Damage", order = 1)]
public class DamagePowerUp : PowerUpSO
{
    public override void ApplyPowerUp(GameObject target)
    {
        target.GetComponent<WeaponController>().ApplyPowerUp(this);
    }
}
