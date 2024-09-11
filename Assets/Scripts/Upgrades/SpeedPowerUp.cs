using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedPowerUp", menuName = "ScriptableObjects/PowerUps/Speed", order = 1)]
public class SpeedPowerUp : PowerUpSO
{
    public override void ApplyPowerUp(GameObject target)
    {
        target.GetComponent<PlayerStats>().ApplyPowerUp(this);
    }
}
