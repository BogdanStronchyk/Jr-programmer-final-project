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
        score = 10;
    }

    public override void GetDamage(int damage)
    {
        health -= damage;
    }

    protected override void CheckHealth()
    {
        if (health <= 0)
        {
            isDead = true;
            Player.Instance.GetScore(score);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        if (!isDead)
        {
            SeekPlayer();

            if (distance <= perceprionRange)
            {
                transform.LookAt(Player.Instance.transform);
                Move();

                if (distance > chargeZone)
                {
                    readyToCharge = true;
                }
                else
                {
                    Charge();
                }

                if (distance <= damageZone)
                {
                    DealDamage();
                }
                else
                {
                    StopDealingDamage();
                }
            }
        }

        
    }
}
