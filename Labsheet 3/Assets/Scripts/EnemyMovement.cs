using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody enemyRB;
    private static Vector3 direction = new Vector3(1,0,0);
    private EnemyController enemyController;

    private bool hasHitEdge = false;

    // screen boundary
    public float boundaryX = 10f;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        enemyController = FindObjectOfType<EnemyController>();
    }

    private void FixedUpdate()
    {
        // Move horizontally
        enemyRB.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

        // Right edge
        if (transform.position.x >= boundaryX && direction.x > 0 && !hasHitEdge)
        {
            HandleEdgeHit(-1);
        }
        // Left edge
        else if (transform.position.x <= -boundaryX && direction.x < 0 && !hasHitEdge)
        {
            HandleEdgeHit(1);
        }

        // Get the absoulute value to handle the negative numbers
        if (Mathf.Abs(transform.position.x) < boundaryX - 0.5f)
        {
            hasHitEdge = false;
        }
    }

    private void HandleEdgeHit(int newDir)
    {
        hasHitEdge = true;
        direction = new Vector3(newDir, 0, 0);
        enemyController.MoveEnemiesDown();
    }
}
