using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all guns
/// </summary>
public abstract class Gun : MonoBehaviour
{
    protected float fireRate;
    protected float reloadTime;
    protected bool isShooting;
    protected int damage;
    public string GunType { get; set; }
    public float maxAmmo;
    public bool isAutomatic;

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

    /// <summary>
    /// The shot event itself; raycasting is used for the sake of simplicity
    /// </summary>
    protected void Shot()
    {
        if (currentAmmo > 0)
        {
            Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // if player doesn't shot the ground, because it doesn't have rigidbody
                if (hit.rigidbody != null && hit.rigidbody.CompareTag("Enemy"))
                {
                    EnemyBehaviour Enemy = hit.rigidbody.gameObject.GetComponent<EnemyBehaviour>();
                    Enemy.GetDamage(damage);
                }
            }
            currentAmmo -= 1;
        }
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
