using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    private float bulletSpeed = 50f;
    public Vector3 flightDirection;
    private Rigidbody bulletRB;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        bulletRB.AddForce(flightDirection * bulletSpeed, ForceMode.Impulse);
    }
}
