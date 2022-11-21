using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for any enemy
/// </summary>
public abstract class EnemyBehaviour : MonoBehaviour
{
    protected int scoreForEnemy;
    protected int damage;
    protected int health; // enemy health

    protected float attackRate;
    protected float distance; // distance between palyer and enemy
    protected float speed = 5f;
    protected float chargeForce = 10f;
    protected float jumpForce = 5f;
    protected float chargeZone = 7f;
    protected float damageZone = 2f;
    protected float perceprionRange = 15f;
    protected Vector3 direction; // enemy movement direction towards player

    private Rigidbody EnemyRB;
    protected bool readyToCharge = false;
    protected bool dealingDamage = false;
    protected bool isOnGround = false;
    
    public bool isDead = false;
    public int level = 1;
    public int score;

    private void Awake()
    {
        EnemyRB = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// General enemy movement
    /// </summary>
    protected virtual void Move()
    {
        if (isOnGround && distance >= damageZone)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }
        
    }

    /// <summary>
    /// Damage dealing method. To be called in child class
    /// </summary>
    protected void DealDamage()
    {
        if (!dealingDamage)
        {
            InvokeRepeating("DamageDealer", 0.3f, attackRate);
            dealingDamage = true;
        }

    }

    protected void StopDealingDamage()
    {
        CancelInvoke();
        dealingDamage = false;
    }

    /// <summary>
    /// Damage dealer metod. To be called repeatedly from DealDamageIfReady() method
    /// </summary>
    protected virtual void DamageDealer()
    {
        Player.Instance.health -= damage * level;
    }

    /// <summary>
    /// Get score for killing an enemy
    /// </summary>
    public virtual void GetScore()
    {
        Player.Instance.Score += scoreForEnemy * level;
    }

    /// <summary>
    /// Damage intake method. To be overwritten
    /// </summary>
    public abstract void GetDamage(int damage);

    /// <summary>
    /// General attacking behavoiur
    /// </summary>
    protected virtual void Charge()
    {
        if (readyToCharge)
        {
            EnemyRB.AddForce(direction * chargeForce, ForceMode.Impulse);
            EnemyRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            readyToCharge = false;
            isOnGround = false;
        }
    }

    /// <summary>
    /// Health check and death if less then 0
    /// </summary>
    protected abstract void CheckHealth();
    

    /// <summary>
    /// Updates distance direction.
    /// Basically works as an enemy perception
    /// </summary>
    protected void SeekPlayer()
    {
        Vector3 PlayerPosition = Player.Instance.transform.position;
        Vector3 EnemyPosition = transform.position;

        direction = (PlayerPosition - EnemyPosition).normalized;
        distance = (PlayerPosition - EnemyPosition).magnitude;
        
    }

    /// <summary>
    /// Check wether enemy touches the ground
    /// </summary>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isOnGround = true;
        }
    }


}
