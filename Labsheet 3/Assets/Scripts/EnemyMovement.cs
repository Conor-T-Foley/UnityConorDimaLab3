using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody enemyRB;
    static private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        direction = new Vector3(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {

        enemyRB.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);


        if (enemyRB.transform.position.x >= 10)
        {
            
            direction = new Vector3(-1, 0, 0);
        }
        else if (enemyRB.transform.position.x <= -10)
        {
            direction = new Vector3(1, 0, 0);
        }
    }

}
