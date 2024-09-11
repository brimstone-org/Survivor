using UnityEngine;


/// <summary>
/// scriptable object for the status effect SO
/// </summary>
[CreateAssetMenu(fileName = "StatusEffect", menuName = "ScriptableObjects/StatusEffects", order = 1)]
public class StatusEffectSO : ScriptableObject
{
    [SerializeField]
    private string statusEffectName; //name of the effect
    public string StatusEffectName
    {
        get => statusEffectName; private set => statusEffectName = value;
    }
    [SerializeField]
    private float dotAmount; //if the effect deals damage
    public float DotAmount
    {
        get => dotAmount; private set => dotAmount = value;
    }
    [SerializeField]
    private float tickSpeed;
    public float TickSpeed //how often does the effect repeat itself
    {
        get => tickSpeed; private set => tickSpeed = value;
    }

    [SerializeField]
    private float movementPenalty; //if it affects the movement
    public float MovementPenalty
    {
        get => movementPenalty; private set => movementPenalty = value;
    }


    [SerializeField]
    private Color effectColor; //if the target of this effect should change color
    public Color EffectColor
    {
        get => effectColor; private set => effectColor = value;
    }
}