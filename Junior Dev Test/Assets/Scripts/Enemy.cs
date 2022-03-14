using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    private Game _gameLoop;
    public Checkpoint targetCheckpoint;
    
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        _gameLoop = GameObject.Find("Game").GetComponent<Game>();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        
        UpdateAggro(stats.aggroRange, 1 << 7);
        
        if (TargetEntity == null) // ACTUAL TARGET IS CHECKPOINT
        {
            TargetDirection = (targetCheckpoint.transform.position - transform.position).normalized;
            Look(targetCheckpoint.transform.position);
            
            transform.position += TargetDirection * stats.speed * Time.deltaTime;
            
            if (Vector3.Distance(transform.position, targetCheckpoint.transform.position) < 0.1)
            {
                GoNextCheckpoint();
            }
            
        }
        else // ACTUAL TARGET IS ENEMY
        {
            TargetDirection = (TargetEntity.transform.position - transform.position).normalized;
            Look(TargetEntity.transform.position);

            // Check distance to move or launch attack
            if (Vector2.Distance(this.transform.position, TargetEntity.transform.position) > stats.attackRange)
            {
                transform.position += TargetDirection * stats.speed * Time.deltaTime;
            }
            else
            {
                UpdateAttack();
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
    
    private void GoNextCheckpoint()
    {
        
        var newTargetCheckpoint = targetCheckpoint.PickRandomNext();
        if (newTargetCheckpoint == null)
        {
            _gameLoop.loseCounter++;
            Destroy(gameObject);
        }
        else
        {
            targetCheckpoint = newTargetCheckpoint;
        }
    }
}
