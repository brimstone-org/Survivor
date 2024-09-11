using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponSO : ScriptableObject
{
    [SerializeField]
    private string weaponName;
    public string WeaponName
    {
        get => weaponName; private set => weaponName = value;
    }
    [SerializeField]
    private GameObject weaponProjectile;
    public GameObject WeaponProjectile
    {
        get => weaponProjectile; private set => weaponProjectile = value;
    }
    [SerializeField]
    private Sprite weaponSprite;
    public Sprite WeaponSprite
    {
        get => weaponSprite; private set => weaponSprite = value;
    }
    [SerializeField]
    private float weaponDamage;
    public float WeaponDamage
    {
        get => weaponDamage; private set => weaponDamage = value;
    }
    [SerializeField]
    private float weaponCooldown;
    public float WeaponCooldown
    {
        get => weaponCooldown; private set => weaponCooldown = value;
    }
    [SerializeField]
    private float weaponRange;
    public float WeaponRange
    {
        get => weaponRange; private set => weaponRange = value;
    }
    [SerializeField]
    private float weaponProjectileSpeed;
    public float WeaponProjectileSpeed
    {
        get => weaponProjectileSpeed; private set => weaponProjectileSpeed = value;
    }
    [SerializeField]
    private int remainingBullets;
    public int RemainingBullets
    {
        get => remainingBullets; private set => remainingBullets = value;
    }

}