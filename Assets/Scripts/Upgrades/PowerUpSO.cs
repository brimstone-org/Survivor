using UnityEngine;

/// <summary>
/// scriptable object parrent for all the powers ups
/// </summary>
public abstract class PowerUpSO : ScriptableObject
{
    [SerializeField]
    private float amount;
    public float Amount
    {
        get => amount; private set => amount = value;
    }
    //powerups
    public enum PowerUpType
    {
        MaxHealth,
        Speed,
        Damage,
        FireRate,
        PoisonDamage
    }
    [SerializeField]
    private PowerUpType powerUpType;
    public PowerUpType TypeOfPowerUp
    {
        get => powerUpType; private set => powerUpType = value;
    }

    [SerializeField]
    private StatusEffectSO statusEffect;
    public StatusEffectSO StatusEffect
    {
        get => statusEffect; private set => statusEffect = value;
    }


    public abstract void ApplyPowerUp(GameObject target);

}