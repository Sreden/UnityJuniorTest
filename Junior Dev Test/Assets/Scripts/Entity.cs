using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntityStats stats;
    protected Vector3 TargetDirection;
    public bool isFocused = false;
    public bool hasAggro = false;

    protected GameObject TargetEntity = null;

    protected void UpdateAggro(float range, int layer)
    {
        if (TargetEntity != null) return;
        TargetDirection = Vector3.zero;
        hasAggro = false;
        // Check around
        var hitColliders = Physics.OverlapSphere(this.transform.position, range, layer);
        // locate enemy
        foreach (var enemy in hitColliders)
        {
            if (enemy == null) continue;
            var entity = enemy.GetComponent<Entity>();
            if (entity.isFocused) continue;
            // if enemy is not focused focus him
            entity.isFocused = true;
            TargetDirection = (entity.transform.position - transform.position).normalized;
            TargetEntity = enemy.gameObject;
            Look(TargetEntity.transform.position);
            hasAggro = true;
            break;
        }
    }
    
    protected void Look(Vector2 direction)
    {
        var localScale = transform.localScale; 
        if (direction.x >  transform.position.x) // To the right
        {
            localScale = new Vector3(1, localScale.y, localScale.z);
        }
        else if (direction.x < transform.position.x) // To the left
        {
            localScale = new Vector3(-1, localScale.y, localScale.z);
        }
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(0f, 1f, 0.11f, 0.56f);
        Gizmos.DrawSphere(transform.position, stats.aggroRange);
    }
}
