using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScrypt : MonoBehaviour
{

    public float fireRate;
    public float blowback = 10;
    private bool isShooting;

    private void Shot()
    {
        Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // if player doesn't shot the ground, because it doesn't have rigidbody
            if (hit.rigidbody != null && hit.rigidbody.CompareTag("Enemy"))
            {
                //hit.rigidbody.AddForce(gameObject.transform.localEulerAngles.normalized * blowback);
                Debug.Log($"You hit {hit.rigidbody.name}");
            }
        }
    }

    public void Fire()
    {
        if (isShooting)
        {
            InvokeRepeating("Shot", 0f, fireRate);
            isShooting = false;
        }
    }

    public void HoldFire()
    {
        CancelInvoke();
        isShooting = true;
    }

}
