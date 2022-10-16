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
        if ((gameObject.transform.position - GameObject.Find("Player").transform.position).magnitude > bulletRange)
        {
            bulletRB.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

}
