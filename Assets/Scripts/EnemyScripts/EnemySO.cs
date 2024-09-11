using UnityEngine;


/// <summary>
/// scriptable objects for enemy stats
/// </summary>
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemies", order = 1)]
public class EnemySO : ScriptableObject
{
    [SerializeField]
    private string enemyName;
    public string EnemyName { get =>enemyName; set =>enemyName = value; }
    [SerializeField]
    private float enemyDamage;
    public float EnemyDamage { get => enemyDamage; set => enemyDamage = value; }
    [SerializeField]
    private float enemySpeed;
    public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
    [SerializeField]
    private float enemyMaxHealth;
    public float EnemyMaxHealth { get => enemyMaxHealth; set => enemyMaxHealth = value; }

}