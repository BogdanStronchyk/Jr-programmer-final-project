using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all guns
/// </summary>
public abstract class Gun : MonoBehaviour
{
    [Header("General stats")]
    protected float fireRate;
    protected float reloadTime;
    protected bool isShooting;
    protected int damage;
    protected int bulletsPerShot;

    protected float m_inaccuracy;
    public float inaccuracy 
    { 
        get 
        {
            return m_inaccuracy;
        }
        set
        {
            if (value > 0.1f || value < 0.01f)
            {
                m_inaccuracy = Mathf.Clamp(value, 0.01f, 0.1f);
            }
        }
    }
    public string GunType { get; set; }
    public float maxAmmo { get; set; }
    public bool isAutomatic { get; set; }

    private float m_currentAmmo;
    public float currentAmmo
    {
        get { return m_currentAmmo; }
        set
        {
            if (value > 0)
            {
                m_currentAmmo = value;
            }
            else
            {
                m_currentAmmo = 0;
            }

        }
    }


    public List<Vector3> hitPointCoords;
    /// <summary>
    /// The shot event itself; raycasting is used for the sake of simplicity
    /// </summary>
    protected virtual void Shot()
    {
        hitPointCoords.Clear();
        if (currentAmmo > 0)
        {
            Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(GetBulletDirection());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody != null && hit.rigidbody.CompareTag("Enemy"))
                {
                    EnemyBehaviour Enemy = hit.rigidbody.gameObject.GetComponent<EnemyBehaviour>();
                    Enemy.GetDamage(damage);
                    Enemy.GetScore();
                }

                else if (hit.collider.CompareTag("Terrain"))
                {
                    hitPointCoords.Add(hit.point);
                }
            }
            currentAmmo -= 1;
        }
    }

    protected virtual void Shot(int rays)
    {
        if (currentAmmo > 0)
        {

            for (int i = 0; i < rays; i++)
            {

                Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(GetBulletDirection());
                
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // if player doesn't shot the ground, because it doesn't have rigidbody
                    if (hit.rigidbody != null && hit.rigidbody.CompareTag("Enemy"))
                    {
                        EnemyBehaviour Enemy = hit.rigidbody.gameObject.GetComponent<EnemyBehaviour>();
                        Enemy.GetDamage(damage);
                        Enemy.GetScore();
                    }

                    else if (hit.collider.CompareTag("Terrain"))
                    {
                        hitPointCoords.Add(hit.point);
                    }
                }
            }
            currentAmmo -= 1;
        }
    }

    Vector3 GetBulletDirection()
    {
        Vector3 targetPos = new Vector3(0.5f, 0.5f, 0);

        targetPos = new Vector3
            (
                targetPos.x + Random.Range(-inaccuracy, inaccuracy),
                targetPos.y + Random.Range(-inaccuracy, inaccuracy),
                0f
            );
        return targetPos;
    }

    /// <summary>
    /// General method for firing the gun; to be overwritten
    /// </summary>
    public abstract void Fire();


    /// <summary>
    /// Use this method to hold automatic fire, when it's invoked repeatedly 
    /// </summary>
    public void HoldFire()
    {
        CancelInvoke();
        isShooting = true;
    }

    /// <summary>
    /// Reloading method. Same to any type of firearm
    /// </summary>
    public void Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            StartCoroutine(ReloadTimer());
        }
            
    }

    /// <summary>
    /// Reloading timer
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
    }

    /// <summary>
    /// Shot delay for single-shot firearms to prevent rapid fire
    /// </summary>
    /// <returns></returns>
    protected IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(fireRate);
        isShooting = true;
    }

}
