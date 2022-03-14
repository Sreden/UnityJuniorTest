using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Statistics", menuName = "Tower Statistics", order = 1)]
public class TowerStats : ScriptableObject
{
    public float buildTime;
    public int cost;

    // not used by damage towers
    public float spawnTime;

    // not used by warrior towers
    public float damage;
    public float attackSpeed;
    public float attackRange;
}
