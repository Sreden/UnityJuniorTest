using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    private Game _gameLoop;
    public Checkpoint targetCheckpoint;
    // Start is called before the first frame update
    private void Start()
    {
        _gameLoop = GameObject.Find("Game").GetComponent<Game>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateAggro(stats.aggroRange, 1 << 7);
        
        if (TargetEntity == null)
        {
            TargetDirection = (targetCheckpoint.transform.position - transform.position).normalized;
            Look(targetCheckpoint.transform.position);
        }
        else
        {
            TargetDirection = (TargetEntity.transform.position - transform.position).normalized;
            Look(TargetEntity.transform.position);
        }
        
        transform.position += TargetDirection * stats.speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetCheckpoint.transform.position) < 0.1)
        {
            GoNextCheckpoint();
        }
        
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
