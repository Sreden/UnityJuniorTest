using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Entity
{
    // Update is called once per frame
    private void Update()
    {
        UpdateAggro(stats.aggroRange, 1 << 6);
        transform.position += TargetDirection * stats.speed * Time.deltaTime;
    }
}
