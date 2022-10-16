using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    public float fireRate;
    private bool isShooting;

    private void Shoot()
    {
        Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log($"You hit {hit.rigidbody}");
        }
    }

    public void Fire()
    {
        if (isShooting)
        {
            InvokeRepeating("Shoot", 0f, fireRate);
            isShooting = false;
        }
    }

    public void HoldFire()
    {
        CancelInvoke();
        isShooting = true;
    }

}
