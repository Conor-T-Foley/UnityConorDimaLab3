using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    private int wave = 1;


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
            float startZ = 2.0f;

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPos = new Vector3(startX + (i * 2.0f), 1, startZ);
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    
}
