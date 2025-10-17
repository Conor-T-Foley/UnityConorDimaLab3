using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float changeTarget = 2.0f;
    public float boundaryX = 10.0f;
    public float boundaryZ = 4.0f;

    private Vector3 targetPosition;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        pickNewTargetArea();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f || timer > changeTarget)
        {
            pickNewTargetArea();
        }
    }

    void pickNewTargetArea()
    {
        float xPos = Random.Range(-boundaryX, boundaryX);
        float zPos = Random.Range(-boundaryZ, boundaryZ);
        targetPosition = new Vector3(xPos,transform.position.y, zPos);
        timer = 0;
    }
}
