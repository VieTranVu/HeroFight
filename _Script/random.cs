using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{

    // Save prefab Enemy
    public GameObject[] enemyPrefabs;
    public GameObject[] itemPrefabs;
    // Position X to Y)
    public float spawnMinX = -10f;  // min X
    public float spawnMaxX = 10f;   // max X
    public float spawnMinY = -5f;   // min Y
    public float spawnMaxY = 5f;    // max Y

    // Time Scale
    public float spawnInterval = 2f;

    private int countEnemy = 3;
    private int countItem = 5;
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", 0, spawnInterval);
        InvokeRepeating("SpawnRandomItem", 0, spawnInterval);
    }

    void SpawnRandomEnemy()
    {
        // Check Enemy Prefab 
        if (enemyPrefabs.Length == 0) return;

        if (countEnemy-- > 0)
        {

            // Choose EnemyPrefab
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject randomEnemy = enemyPrefabs[randomEnemyIndex];

            // Set Position X and Y
            float randomX = Random.Range(spawnMinX, spawnMaxX);
            float randomY = Random.Range(spawnMinY, spawnMaxY);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);  // 

            // Random Enemy Prefab
            Instantiate(randomEnemy, randomPosition, Quaternion.identity);
        }
           
    }
    void SpawnRandomItem()
    {
        // Check Enemy Prefab 
        if (itemPrefabs.Length == 0) return;
        if (countItem-- > 0)
        {
            // Choose EnemyPrefab
            int randomItemIndex = Random.Range(0, itemPrefabs.Length);
            GameObject randomItem = itemPrefabs[randomItemIndex];

            // Set Position X and Y
            float randomX = Random.Range(spawnMinX, spawnMaxX);
            float randomY = Random.Range(spawnMinY, spawnMaxY);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);  // 

            // Random Enemy Prefab
            Instantiate(randomItem, randomPosition, Quaternion.identity);
        }
    }
}
