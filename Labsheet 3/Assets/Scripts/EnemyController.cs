using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    private int wave = 2; // 1 = block enemies, 2 = circular , 3 = boss
    private List<GameObject> activeEnemies = new List<GameObject>();
    public float enemyDropMagnitude = 0.5f;
    public float circularEnemySpread = 1.5f;

    void Start()
    {
        SpawnEnemies();
    }

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
        else if (wave == 2)
        {
            Vector3 focalPoint = new Vector3(0, 1, 4); 
            float radius = 0.5f;
            float angleStep = 360f / enemyCount;

            for (int i = 0; i < enemyCount; i++)
            {

                float angle = i * angleStep;
                float radians = angle * Mathf.Deg2Rad;

                Vector3 spawnPos = new Vector3(
                    focalPoint.x + Mathf.Cos(radians) * radius * circularEnemySpread,
                    focalPoint.y,
                    focalPoint.z + Mathf.Sin(radians) * radius * circularEnemySpread
                );

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                activeEnemies.Add(enemy);


                CircularEnemyMovement circularMove = enemy.AddComponent<CircularEnemyMovement>();
                circularMove.focalPoint = focalPoint;
                circularMove.radius = radius;

                circularMove.startAngle = angle;

                circularMove.rotationSpeed = 30f;
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
