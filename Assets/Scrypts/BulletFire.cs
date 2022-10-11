using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    public float fireRate;
    public float bulletRange = 100f;
    private bool isShooting;


    private void ActivateBullet()
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();
        if (obj == null) return;

        obj.transform.position = GameObject.Find("BuletSpawner").transform.position;
        obj.transform.rotation = GameObject.Find("BuletSpawner").transform.rotation;
        obj.GetComponent<BulletBehavior>().flightDirection = GetFlightDirection();
        obj.SetActive(true);
        StartCoroutine(Deactivate(obj, 0.7f));
    }

    private Vector3 GetFlightDirection()
    {

        Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(bulletRange);
        }
        Vector3 flightDirection = (targetPoint - GameObject.Find("BuletSpawner").transform.position).normalized;
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

    IEnumerator Deactivate(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

}
