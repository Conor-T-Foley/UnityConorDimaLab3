using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularEnemyMovement : MonoBehaviour
{
    public Vector3 focalPoint;
    public float rotationSpeed = 30f;
    public float radius = 5f;
    public float startAngle = 0f;

    private float currentAngle;

    void Start()
    {
        currentAngle = startAngle;
    }

    void Update()
    {
        // Rotate around the focal point
        currentAngle += rotationSpeed * Time.deltaTime;
        float radians = currentAngle * Mathf.Deg2Rad;

        Vector3 newPos = new Vector3(
            focalPoint.x + Mathf.Cos(radians) * radius,
            focalPoint.y,
            focalPoint.z + Mathf.Sin(radians) * radius
        );

        transform.position = newPos;
    }
}
