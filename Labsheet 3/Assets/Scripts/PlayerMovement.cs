using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Vector3 bounceDirection = collision.contacts[0].normal;

            float bounceForce = 2f;
            playerRB.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(forwardInput, 0, 0);
        playerRB.AddForce(movement * speed);
    }
}
