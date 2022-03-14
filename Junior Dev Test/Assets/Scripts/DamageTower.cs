using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DamageTower : Tower
{
    [SerializeField] private Transform attackFrom;
    [SerializeField] private GameObject projectile;
    private float _elapsedTimeBetweenAttack;
    private GameObject _targetEntity = null;
    private new void Update()
    {
        base.Update();
        
        if (!_isBuilt) return;
        
        // Select enemy layer
        UpdateAggro(1 << 6);

        if (_targetEntity == null) return;
        UpdateAttack();
    }

    private void UpdateAttack()
    {
        _elapsedTimeBetweenAttack += Time.deltaTime;
        // cancel if elapsedTime is not enough
        if (_elapsedTimeBetweenAttack < stats.attackSpeed) return;
        
        LaunchProjectile();
        _elapsedTimeBetweenAttack = 0;
    }
    
    private void UpdateAggro(int layer)
    {
        if (_targetEntity != null)
        {
            if (!(Vector2.Distance(_targetEntity.transform.position, 
                    attackFrom.position) > stats.attackRange)) return;
            CheckForEnemy(layer);
        }
        CheckForEnemy(layer);
    }


    private void CheckForEnemy(int layer)
    {
        // Check around
        // 10 is a maximum number for research
        var hitColliders = new Collider2D[10];
        Physics2D.OverlapCircleNonAlloc(this.transform.position, stats.attackRange, hitColliders, layer);
        // locate enemy
        foreach (var enemy in hitColliders)
        {
            if (enemy == null) continue;
            _targetEntity = enemy.gameObject;
            break;
        }
    }

    private void LaunchProjectile()
    {
        var newProjectile = Instantiate(projectile, attackFrom);
        newProjectile.AddComponent<Projectile>().Setup(attackFrom.position,
            _targetEntity, stats.damage,0.5f);
    }
    
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1f, 0f, 0.47f, 0.28f);
        Gizmos.DrawSphere(attackFrom.position, stats.attackRange);
    }
    
}
