using System;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] public TowerStats stats;
    [SerializeField] private GameObject loader;
    [SerializeField] private GameObject towerPrefab;
    protected bool _isBuilt;

    private float _elapsedTime;

    protected void Start()
    {
        towerPrefab.SetActive(false);
    }

    protected void Update()
    {
        if (!_isBuilt) UpdateLoader();

        if (_elapsedTime < stats.buildTime)
        {
            _elapsedTime += Time.deltaTime;
        } else if (!_isBuilt)
        {
            _isBuilt = true;
            loader.SetActive(false);
            towerPrefab.SetActive(true);
        }
    }

    private void UpdateLoader()
    {
        var percent = _elapsedTime / stats.buildTime;
        loader.GetComponent<Slider>().value = percent;
    }
}
