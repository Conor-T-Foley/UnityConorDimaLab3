// EnemyController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    public int wave = 2; // 1 = block enemies, 2 = circular , 3 = boss
    private List<GameObject> activeEnemies = new List<GameObject>();
    public float enemyDropMagnitude = 0.5f;
    public float circularEnemySpread = 1.5f;

    // Make circular enemies share a focal point so the drop together!!
    public Vector3 circularFocalPoint = new Vector3(0, 1, 4);

    void Start()
    {
        SpawnEnemies();
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
            float radius = 0.5f;
            float angleStep = 360f / enemyCount;

            for (int i = 0; i < enemyCount; i++)
            {
                float angle = i * angleStep;
                float radians = angle * Mathf.Deg2Rad;

                Vector3 spawnPos = new Vector3(
                    circularFocalPoint.x + Mathf.Cos(radians) * radius * circularEnemySpread,
                    circularFocalPoint.y,
                    circularFocalPoint.z + Mathf.Sin(radians) * radius * circularEnemySpread
                );

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                activeEnemies.Add(enemy);

               
                CircularEnemyMovement circularMove = enemy.AddComponent<CircularEnemyMovement>();
                circularMove.focalPointRef = this;
                circularMove.radius = radius;
                circularMove.startAngle = angle;
                circularMove.rotationSpeed = 30f;
                circularMove.speed = 1.0f;
                circularMove.boundaryX = 10f;
            }
        }
    }

    public void MoveEnemiesDown()
    {
        
        circularFocalPoint += Vector3.back * enemyDropMagnitude;

        
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null && enemy.GetComponent<CircularEnemyMovement>() == null)
            {
                enemy.transform.position += Vector3.back * enemyDropMagnitude;
            }
        }
    }
}
