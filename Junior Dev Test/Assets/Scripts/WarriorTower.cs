using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTower : Tower
{
    [SerializeField] private GameObject warriorPrefab;
    [SerializeField] public GameObject[] spawns;
    public GameObject[] _warriors;

    public float _elapsedTimeBetweenSpawns;

    private new void Start()
    {
        base.Start();
        
        _warriors = new [] {(GameObject)null, null, null};
    }
    private new void Update()
    {
        base.Update();

        if (!_isBuilt) return;
        
        _elapsedTimeBetweenSpawns += Time.deltaTime;
        if (!(_elapsedTimeBetweenSpawns > stats.spawnTime)) return;
            
        if (Spawn()) _elapsedTimeBetweenSpawns = 0;
    }

    private bool Spawn()
    {
        for (var i = 0; i < _warriors.Length; i++)
        {
            var warrior = _warriors[i];
            
            if (warrior != null) continue;
            _warriors[i] = Instantiate(warriorPrefab, spawns[i].transform);
            _warriors[i].GetComponent<Ally>().spawnTarget = spawns[i];

            return true;
        }
        return false;
    }
    
    


}
