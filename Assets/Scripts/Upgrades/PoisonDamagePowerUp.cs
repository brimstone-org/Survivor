
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonDamagePowerUp", menuName = "ScriptableObjects/PowerUps/PoisonDamage", order = 1)]
public class PoisonDamagePowerUp : PowerUpSO
{
    public override void ApplyPowerUp(GameObject target)
    {
        target.GetComponent<WeaponController>().ApplyPowerUp(this);
    }
}
