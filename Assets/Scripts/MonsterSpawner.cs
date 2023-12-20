using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject enemyPrefab;
    public int maxMonsters = 10;
    private int currentMonsterCount = 0;

    public float minX = 14f; // Minimum X position
    public float maxX = 16f; // Maximum X position
    public float minY = 1.1f; // Minimum Y position
    public float maxY = 3.0f; // Maximum Y position

    void SpawnEnemy()
    {
        if (enableSpawn && currentMonsterCount < maxMonsters)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Instantiate(enemyPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            currentMonsterCount++;
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 1f);
    }

    void Update()
    {
        // Additional update logic if needed
    }
}
