using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    public float fireRate;
    private bool isShooting;


    // Update is called once per frame
    private void ActivateBullet()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (obj == null) return;

        obj.transform.position = GameObject.Find("BuletSpawner").transform.position;
        obj.transform.rotation = GameObject.Find("BuletSpawner").transform.rotation;
        obj.SetActive(true);
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
