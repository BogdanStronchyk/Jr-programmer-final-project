using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public Vector3 flightDirection;
    public float bulletRange = 100f;
    
    private float bulletSpeed = 150f;
    private Rigidbody bulletRB;
    
    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        bulletRB.AddForce(flightDirection * bulletSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        Vector3 bulletPosition = transform.position;
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        float distance = (bulletPosition - playerPosition).magnitude;

        if (distance > bulletRange)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // Disable bullet velocity to prevent accelerating on reactivation
        bulletRB.velocity = Vector3.zero;
    }

}
