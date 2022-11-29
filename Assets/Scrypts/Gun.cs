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

    [Header("Reloading flags")]
    private bool gunDry = false;
    private bool gunSemidry = false;
    private bool haveMag = false;

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
    public int maxAmmo { get; set; }

    public bool reloading;
    public bool isAutomatic { get; set; }

    private int m_currentAmmo;
    public int currentAmmo
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
    [Header("Sounds")]
    public AudioClip ShotSound;
    public AudioClip ReloadSound;
    private AudioSource source;

    private void Start()
    {
        source = FindObjectOfType<AudioSource>();
        Debug.Log(source);
    }

    /// <summary>
    /// The shot event itself; raycasting is used for the sake of simplicity
    /// </summary>
    protected virtual void Shot()
    {
        hitPointCoords.Clear();
        if (currentAmmo > 0)
        {
            source.PlayOneShot(ShotSound);
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
            source.PlayOneShot(ShotSound);
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
        if (currentAmmo < maxAmmo && Player.Instance.ammunition > 0 && !reloading)
        {
            source.PlayOneShot(ReloadSound);
            StartCoroutine(DelayedReload());
            reloading = true;
        }  
    }

    private void GunStateCheck()
    {
        // checking the state of the gun
        if (currentAmmo == maxAmmo)
        {
            gunDry = false;
            gunSemidry = false;
        }
        else if (currentAmmo < maxAmmo && currentAmmo > 0)
        {
            gunDry = false;
            gunSemidry = true;
        }
        else if (currentAmmo == 0)
        {
            gunDry = true;
            gunSemidry = false;
        }

        // checking if player have enough ammo to form at least 1 mag
        if (Player.Instance.ammunition / maxAmmo >= 1)
        {
            haveMag = true;
        }
        else
        {
            haveMag = false;
        }


        
    } 
    private void GunReload()
    {
        if (haveMag)
        {
            if (gunDry)
            {
                Debug.Log($"Gun dry & have mag");
                Player.Instance.ammunition -= maxAmmo;
                currentAmmo = maxAmmo;
            }

            else if (gunSemidry)
            {
                Debug.Log($"Gun semidry & have mag");
                Player.Instance.ammunition -= (maxAmmo - currentAmmo);
                currentAmmo = maxAmmo;
            }
        }

        else
        {

            if (gunDry)
            {
                Debug.Log($"No mag & dry");
                currentAmmo += Player.Instance.ammunition;
                Player.Instance.ammunition = 0;
            }

            if (gunSemidry)
            {
                Debug.Log($"No mag & semidry");
                if (currentAmmo + Player.Instance.ammunition > maxAmmo)
                {
                    Player.Instance.ammunition -= (maxAmmo - currentAmmo);
                    currentAmmo = maxAmmo;
                }
                else
                {
                    currentAmmo += Player.Instance.ammunition;
                    Player.Instance.ammunition = 0;
                }
            }

        }
    }

    /// <summary>
    /// Reloading timer
    /// </summary>
    IEnumerator DelayedReload()
    {
        yield return new WaitForSeconds(reloadTime);
        GunStateCheck();
        GunReload();
        reloading = false;
    }

    /// <summary>
    /// Shot delay for single-shot firearms to prevent rapid fire
    /// </summary>
    protected IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(fireRate);
        isShooting = true;
    }

}
