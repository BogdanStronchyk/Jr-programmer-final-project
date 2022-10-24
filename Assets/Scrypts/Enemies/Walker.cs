using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// INHERITANCE EXAMPLE
/// </summary>
public class Walker : EnemyBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        health = 20;
        damage = 10;
        attackRate = 0.5f;
    }

    protected override void GetDamage(int damage)
    {
        health -= damage;
    }

    protected override void DealDamage()
    {
        Player.Instance.health -= damage * level;
    }

    

    // Update is called once per frame
    void Update()
    {
        direction = (Player.Instance.transform.position - transform.position).normalized;
        distance = (Player.Instance.transform.position - transform.position).magnitude;
        transform.LookAt(Player.Instance.transform);
        if (distance > damageZone)
        {
            CancelInvoke(); // cancelling damage dealing when leaving the damage zone
            hasAttacked = false;
            dealingDamage = false;
        }

        if (distance <= perceprionRange)
        {
            Move();
            
            if (distance <= chargeZone)
            {
                Attack();
            }

            else if (distance <= damageZone)
            {
                if (!dealingDamage)
                {
                    InvokeRepeating("DealDamage", 0f, attackRate);
                    dealingDamage = true;
                }
            }
        }
            

        CheckHealth();
    }
}
