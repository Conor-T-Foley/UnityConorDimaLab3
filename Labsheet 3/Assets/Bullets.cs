using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Collision detected between player and enemy");
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward* speed * Time.deltaTime);
    }
}
