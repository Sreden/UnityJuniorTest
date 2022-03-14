using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 _from;
    private GameObject _to;
    private Vector2 _lastKnownPosition;
    public float pathTimer;
    private float _timeToTouch;
    private float _damage;

    public void Setup (Vector2 from, GameObject to, float damage,  float timeToTouch)
    {
        _from = from;
        _to = to;
        _damage = damage;
        _timeToTouch = timeToTouch;
        _lastKnownPosition = _to.transform.position;
    }

    private void Update()
    {
        if (_to != null) _lastKnownPosition = _to.transform.position;
        
        pathTimer += Time.deltaTime;
        var percent = pathTimer / _timeToTouch;
        transform.position = new Vector2(Mathf.Lerp(_from.x, _lastKnownPosition.x, pathTimer / _timeToTouch), 
            Mathf.Lerp(_from.y, _lastKnownPosition.y, pathTimer / _timeToTouch));

        if (!(pathTimer >= _timeToTouch)) return;
        
        if (_to != null)
        {
            _to.GetComponent<Entity>().Damage(_damage);
        }
        Destroy(gameObject);
    }
}
