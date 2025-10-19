using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1.5f;
    public float bulletSpeed = 10.0f;
    public bool trackPlayer = false; // Make this true in wave 3 to make the enemies shoot at the player!!

    private Transform player;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
        player = thePlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0.0f;
        }
    }


    void Shoot()
    {
        Vector3 direction;

        if (trackPlayer)
        {
            direction = (player.position - transform.position).normalized;
        }
        else
        {
            direction = Vector3.back; // Should make the wave 1&2 shoot straight down
        }

        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed;

        Destroy(bullet, 2.0f);


    }
}
