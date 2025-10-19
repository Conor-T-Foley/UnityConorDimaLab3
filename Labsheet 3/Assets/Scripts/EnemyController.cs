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

    public GameObject projectilePrefab;
    public float fireRate = 1.5f;
    public float bulletSPeed = 10.0f;

    private bool ongoingWave = false;

    void Start()
    {
        SpawnEnemies();
        ongoingWave = true;
    }

    private void Update()
    {
        // For each enemy in the list, if enemey is null, remove it
        // Destroying enemy with player bullet just sets it to null so a count check wont work
        // Have to have this check instead!!
        activeEnemies.RemoveAll(e => e == null);

        if (ongoingWave && activeEnemies.Count == 0)
        {
            ongoingWave = false;
            wave++;
            StartCoroutine(NextWave());
        }

    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(2.0f);
        SpawnEnemies();
        ongoingWave= true;
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
                enemy.tag = "enemy";
                activeEnemies.Add(enemy);

                AddShooting(enemy, trackPlayer: false);
            }
        }
        else if (wave == 2)
        {
            float radius = 1.0f;
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
                enemy.tag = "enemy";
                activeEnemies.Add(enemy);


                CircularEnemyMovement circularMove = enemy.AddComponent<CircularEnemyMovement>();
                circularMove.focalPointRef = this;
                circularMove.radius = radius;
                circularMove.startAngle = angle;
                circularMove.rotationSpeed = 30f;
                circularMove.speed = 1.0f;
                circularMove.boundaryX = 10f;

                AddShooting(enemy, trackPlayer: false);
            }
        }
        else if (wave == 3)
        {
            float startZ = 4.0f;

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-5.0f, 5.0f), 1, startZ + Random.Range(-2.0f, 2.0f));
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemy.tag = "enemy";
                activeEnemies.Add(enemy);

                BossEnemyMovement bossMovement = enemy.AddComponent<BossEnemyMovement>();
                bossMovement.speed = Random.Range(2.0f, 4.0f);

                AddShooting (enemy, trackPlayer: true);
            }
        }

        void AddShooting(GameObject enemy, bool trackPlayer)
        {
            EnemyShooting shooting = enemy.AddComponent<EnemyShooting>();
            shooting.projectilePrefab = projectilePrefab;
            shooting.fireRate = fireRate;
            shooting.bulletSpeed = bulletSPeed;
            shooting.trackPlayer = trackPlayer;
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


