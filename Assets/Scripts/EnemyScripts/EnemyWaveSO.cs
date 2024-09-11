using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Scriptable Object for the wave data
/// </summary>
[CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWaves", order = 1)]
public class EnemyWaveSO : ScriptableObject
{
    /// <summary>
    /// info regarding the way to spawn a specific enemey
    /// </summary>
    [System.Serializable]
    public class EnemyInfo
    {
        [Range(0,1f)]
        public float enemySpawnProbability; //the number of enemies to spawn per wave
        public GameObject enemyPrefab; //prefab to spawn
    }



    [SerializeField]
    private List<EnemyInfo> enemies; //list of enemies spawned in this wave
    public List<EnemyInfo> Enemies
    {
        get => enemies; private set => enemies = value;
    }


    [SerializeField]
    private float timeBeforeThisWaveStarts; //the time before this wave starts
    public float TimeBeforeThisWaveStarts
    {
        get => timeBeforeThisWaveStarts; private set => timeBeforeThisWaveStarts = value;
    }


    [SerializeField]
    private float enemySpawnInterval; //Spawn interval between enemies
    public float EnemySpawnInterval
    {
        get => enemySpawnInterval; private set => enemySpawnInterval = value;
    }

    [SerializeField]
    private int totalEnemies; //total enemies in this wave
    public int TotalEnemies
    {
        get => totalEnemies; private set => totalEnemies = value;
    }
}