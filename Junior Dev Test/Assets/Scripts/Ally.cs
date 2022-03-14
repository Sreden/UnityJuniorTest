using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Entity
{
    public GameObject spawnTarget = null;
    private new void Start()
    {
        base.Start();

        // First attack is instant
        ElapsedTimeBetweenAttacks = stats.attackSpeed;
    }
    
    private new void Update()
    {
        base.Update();
        
        UpdateAggro(stats.aggroRange, 1 << 6);

        if (TargetEntity != null)
        {
            TargetDirection = (TargetEntity.transform.position - transform.position).normalized;
            Look(TargetEntity.transform.position);
            if (Vector2.Distance(this.transform.position, TargetEntity.transform.position) > stats.attackRange)
            {
                transform.position += TargetDirection * stats.speed * Time.deltaTime;
            }
            else
            {
                UpdateAttack();
            }
        }
        else //Target is returning to tower spot
        {
            if (spawnTarget == null) return;
            
            TargetDirection = (spawnTarget.transform.position - transform.position).normalized;
            Look(spawnTarget.transform.position);
            if (Vector2.Distance(this.transform.position, spawnTarget.transform.position) > 0.1)
            {
                transform.position += TargetDirection * stats.speed * Time.deltaTime;
            }
        }
    }
    
    private void UpdateAttack()
    {
        ElapsedTimeBetweenAttacks += Time.deltaTime;
        // cancel if elapsedTime is not enough
        if (ElapsedTimeBetweenAttacks < stats.attackSpeed) return;
        
        TargetEntity.GetComponent<Entity>().Damage(stats.attackDamage);
        ElapsedTimeBetweenAttacks = 0;
    }
}
