using UnityEngine;

[CreateAssetMenu(fileName = "Entity Statistics", menuName = "Entity Statistics", order = 1)]
public class EntityStats : ScriptableObject
{

    public float maxLife;
    
    public float speed;
    public float aggroRange;

    public float attackDamage;
    public float attackRange;
    public float attackSpeed;
}
