using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    public float fireRate;
    private bool isShooting;


    private void ActivateBullet()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (obj == null) return;

        obj.transform.position = GameObject.Find("BuletSpawner").transform.position;
        obj.transform.rotation = GameObject.Find("BuletSpawner").transform.rotation;
        obj.GetComponent<BulletBehavior>().flightDirection = GetFlightDirection();
        obj.SetActive(true);
    }

    private Vector3 GetFlightDirection()
    {
        Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        Vector3 flightDirection = ray.direction.normalized;
        return flightDirection;
    }

    public void Fire()
    {
        if (isShooting)
        {
            InvokeRepeating("ActivateBullet", 0f, fireRate);
            isShooting = false;
        }
    }

    public void HoldFire()
    {
        CancelInvoke();
        isShooting = true;
    }

}
