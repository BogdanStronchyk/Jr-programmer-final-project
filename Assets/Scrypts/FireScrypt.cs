using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScrypt : MonoBehaviour
{

    public float fireRate;
    public float ammo = 30;
    private bool isShooting;
    private int damage = 10;



    private void Shot()
    {
        Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // if player doesn't shot the ground, because it doesn't have rigidbody
            if (hit.rigidbody != null && hit.rigidbody.CompareTag("Enemy"))
            {
                EnemyBehaviour Enemy =  hit.rigidbody.gameObject.GetComponent<EnemyBehaviour>();
                Enemy.GetDamage(damage);
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
