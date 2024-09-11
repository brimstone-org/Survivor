using UnityEngine;


/// <summary>
/// scriptable object for the player stats
/// </summary>
[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerSO : ScriptableObject
{
    [SerializeField]
    private string characterName;
    public string CharacterName
    {
        get => characterName; private set => characterName = value;
    }
    [SerializeField]
    private GameObject weapon;
    public GameObject Weapon
    {
        get => weapon; private set => weapon = value;
    }
    [SerializeField]
    private float maxHealth;
    public float MaxHealth
    {
        get => maxHealth; private set => maxHealth = value;
    }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed; private set => moveSpeed = value;
    }

    [SerializeField]
    private float invincibilityDuration;
    public float InvincibilityDuration
    {
        get => invincibilityDuration; private set => invincibilityDuration = value;
    }

}