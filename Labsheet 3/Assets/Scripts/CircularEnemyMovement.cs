using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularEnemyMovement : MonoBehaviour
{
    
    public EnemyController focalPointRef;

    public float rotationSpeed = 30f;
    public float radius = 5f;
    public float startAngle = 0f;
    public float speed = 0.5f;
    public float boundaryX = 10f;

    private float currentAngle;
    private static Vector3 direction = Vector3.right;
    private bool hasHitEdge = false;

    void Start()
    {
        currentAngle = startAngle;
    }

    void Update()
    {
        if (focalPointRef == null) return;

        focalPointRef.circularFocalPoint += direction * speed * Time.deltaTime;

        if (focalPointRef.circularFocalPoint.x >= boundaryX && direction.x > 0 && !hasHitEdge)
        {
            HandleEdgeHit(-1);
        }
        else if (focalPointRef.circularFocalPoint.x <= -boundaryX && direction.x < 0 && !hasHitEdge)
        {
            HandleEdgeHit(1);
        }

        if (Mathf.Abs(focalPointRef.circularFocalPoint.x) < boundaryX - 0.5f)
        {
            hasHitEdge = false;
        }

        currentAngle += rotationSpeed * Time.deltaTime;
        float radians = currentAngle * Mathf.Deg2Rad;

        transform.position = new Vector3(
            focalPointRef.circularFocalPoint.x + Mathf.Cos(radians) * radius,
            focalPointRef.circularFocalPoint.y,
            focalPointRef.circularFocalPoint.z + Mathf.Sin(radians) * radius
        );
    }

    private void HandleEdgeHit(int newDir)
    {
        hasHitEdge = true;
        direction = new Vector3(newDir, 0, 0);
        focalPointRef.MoveEnemiesDown(); 
    }
}
