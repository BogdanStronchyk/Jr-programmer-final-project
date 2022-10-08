using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    private float bulletRange = 100f;
    private float bulletSpeed = 150f;


    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        if (Vector3.Magnitude(transform.position - GameObject.Find("Player").transform.position) > bulletRange)
        {
            gameObject.SetActive(false);
        }
    }
}
