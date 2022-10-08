using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    private float bulletRange = 100f;
    private float bulletSpeed = 150f;
    private Camera Camera;
    private Vector3 flightDirection;
    private bool isActive = true;

    private void Start()
    {
        Camera = FindObjectOfType<Camera>();
    }

    private void Initialize()
    {
        
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(bulletRange);

        flightDirection = (targetPoint - GameObject.Find("BuletSpawner").transform.position).normalized;

        Debug.Log($"{flightDirection}");
    }
    void Update()
    {
        if (isActive)
        {
            Initialize();
            isActive = false;
        }

        // change Vector3.forward to flightDirection
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        if (Vector3.Magnitude(transform.position - GameObject.Find("Player").transform.position) > bulletRange)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
