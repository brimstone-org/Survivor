using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxHealthPowerUp", menuName = "ScriptableObjects/PowerUps/MaxHealth", order = 1)]
public class MaxHealthPowerUp : PowerUpSO
{
    public override void ApplyPowerUp(GameObject target)
    {
        target.GetComponent<PlayerStats>().ApplyPowerUp(this);
    }
}
