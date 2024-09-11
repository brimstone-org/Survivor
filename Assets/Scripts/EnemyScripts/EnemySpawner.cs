using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using static EnemyWaveSO;

public class EnemySpawner : MonoBehaviour
{
    [Inject]
    PlayerStats playerStats;


    [SerializeField]
    private List<EnemyWaveSO> waves; //list of waves
    private EnemyWaveSO currentWave; //the current wave
    private int currentWaveIndex = 0; //index of the current wave
    private bool stopSpawning = false; //when reached the end of the waves
    private int spawnedEnemiesPerWave; //number of enemies spawned per wave

    float spawnTimer = 0; //timer used to determine the spawn of next enemy
    
    private System.Random random = new System.Random(); //helps choose the enemy to spawn based on their probability

   

    private void Start()
    {
        currentWave = waves[currentWaveIndex];
        SpawnEnemies();
    }


    private void Update()
    {
        if (stopSpawning)
        {
            return;
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentWave.EnemySpawnInterval)
        {
            spawnTimer = 0;
            SpawnEnemies();
        }
    }
   
    /// <summary>
    /// instantiate enemies
    /// </summary>
    void SpawnEnemies()
    {
       
        if (spawnedEnemiesPerWave < currentWave.TotalEnemies)
        {
            EnemyInfo newEnemy = GetRandomEnemy(waves[currentWaveIndex].Enemies);
            if (newEnemy != null)
            {
                GameObject enemy = Instantiate(newEnemy.enemyPrefab, GetEnemySpawnPosition(), Quaternion.identity);
                enemy.GetComponent<EnemyStats>().InitializeEnemy(playerStats, this);
                spawnedEnemiesPerWave++;
            }
        }
        else
        {
            StartCoroutine(SpawnNextWave());
            
           
        }
    }


    /// <summary>
    /// Get Random enemy to spawn based on the probability 
    /// </summary>
    /// <param name="pool"></param>
    /// <returns></returns>
    private EnemyInfo GetRandomEnemy(IEnumerable<EnemyInfo> pool)
    {
        // get universal probability 
        double u = pool.Sum(p => p.enemySpawnProbability);

        // pick a random number between 0 and u
        double r = random.NextDouble() * u;

        double sum = 0;
        foreach (EnemyInfo n in pool)
        {
            // loop until the random number is less than our cumulative probability
            if (r <= (sum = sum + n.enemySpawnProbability))
            {
                return n;
            }
        }
        // should never get here
        return null;
    }

    /// <summary>
    /// get a random spawn point outside of the camera view
    /// </summary>
    /// <returns></returns>
    public Vector2 GetEnemySpawnPosition()
    {
        int spawnSide = Random.Range(0, 4); //which side of the screen to spawn in
        Camera mainCamera = Camera.main;
        float height = mainCamera.orthographicSize * 2;
        float width = height * mainCamera.aspect;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)); //bottom left corner of the camera in world coords
       
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane)); //top right corner of the camera in world coords
        Vector2 spawnPoint = Vector2.zero;
        if (spawnSide == 0) //left side
        {
            
            spawnPoint = new Vector2(bottomLeft.x - Random.Range(0, 15), bottomLeft.y + Random.Range(0, height));
        }
        else if (spawnSide == 1) //top side
        {
            spawnPoint = new Vector2(topRight.x - Random.Range(0, width), topRight.y + Random.Range(0, 15));
        }
        else if(spawnSide == 2) //right side
        {
            spawnPoint = new Vector2(topRight.x + Random.Range(0, 15), topRight.y - Random.Range(0, height));
        }
        else if(spawnSide == 3) //bottom side
        {
            spawnPoint = new Vector2(bottomLeft.x + Random.Range(0, width), bottomLeft.y - Random.Range(0, 15));
        }
        return spawnPoint;
    }

    /// <summary>
    /// spawn the next wave after the wait time in the wave SO
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnNextWave()
    {
        
        yield return new WaitForSeconds(currentWave.TimeBeforeThisWaveStarts);
        {
            if (currentWaveIndex < waves.Count - 1)
            {
                currentWaveIndex++;
                currentWave = waves[currentWaveIndex];
                spawnedEnemiesPerWave = 0;
            }
            else
            {
                stopSpawning = true;
            }
        }
    }
}
