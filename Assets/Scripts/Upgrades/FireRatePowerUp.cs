using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireRatePowerUp", menuName = "ScriptableObjects/PowerUps/FireRate", order = 1)]
public class FireRatePowerUp : PowerUpSO
{
    public override void ApplyPowerUp(GameObject target)
    {
        target.GetComponent<WeaponController>().ApplyPowerUp(this);
    }
}
