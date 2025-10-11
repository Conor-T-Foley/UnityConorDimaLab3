using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    private int wave = 1; // 1 = block enemies, 2 = circular , 3 = boss
    private List<GameObject> activeEnemies = new List<GameObject>();
    public float enemyDropMagnitude = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        if (wave == 1)
        {
            float startX = -((enemyCount - 1) * 1.0f);
            float startZ = 4.0f;

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPos = new Vector3(startX + (i * 2.0f), 1, startZ);
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                activeEnemies.Add(enemy);
            }
        }
    }

    public void MoveEnemiesDown()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                enemy.transform.position += Vector3.back * enemyDropMagnitude;
            }
        }
    }
}
