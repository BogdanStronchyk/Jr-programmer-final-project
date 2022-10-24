using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for any enemy
/// </summary>
public abstract class EnemyBehaviour : MonoBehaviour
{
    protected int health; // enemy health
    protected int damage;

    protected float attackRate;
    protected float distance; // distance between palyer and enemy
    protected float speed = 5f;
    protected float chargeForce = 10f;
    protected float jumpForce = 5f;
    protected float chargeZone = 5f;
    protected float damageZone = 5f;
    protected float perceprionRange = 15f;
    protected Vector3 direction; // enemy movement direction towards player

    private Rigidbody EnemyRB;
    protected bool hasAttacked = false;
    protected bool dealingDamage = false;
    protected bool isOnGround = false;

    public int level;

    private void Awake()
    {
        EnemyRB = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// General enemy movement
    /// </summary>
    protected virtual void Move()
    {
        if (isOnGround && distance >= damageZone * 0.35f)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward); //direction);
        }
        
    }

    /// <summary>
    /// Damage dealing method. To be overwritten
    /// </summary>
    protected abstract void DealDamage();

    /// <summary>
    /// Damage intake method. To be overwritten
    /// </summary>
    protected abstract void GetDamage(int damage);

    /// <summary>
    /// General attacking behavoiur
    /// </summary>
    protected virtual void Attack()
    {
        if (!hasAttacked)
        {
            EnemyRB.AddForce(direction * chargeForce, ForceMode.Impulse);
            EnemyRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasAttacked = true;
            isOnGround = false;
        }
            
    }

    /// <summary>
    /// Health check and death if less then 0
    /// </summary>
    protected virtual void CheckHealth()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Keeps enemy faced towards player when necessary
    /// </summary>
    protected void FaceTowardsPlayer()
    {
        transform.eulerAngles = new Vector3(0f, direction.y, 0f);
    }

    /// <summary>
    /// Enemy behavior when colliding woth player
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Terrain"))
        {
            isOnGround = true;
        }
    }


}
