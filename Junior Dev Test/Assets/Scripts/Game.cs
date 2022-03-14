using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Text goldText;
    public int golds = 100;
    public int maxGolds = 1000;
    
    // + donationAmount to golds every goldDonationRate
    public float goldDonationRate = 1f;
    public int donationAmount = 3;
    private float _elapsedTime;
    
    // Enemies
    [SerializeField] private GameObject enemyPrefab;
    public int loseCounter;
    [SerializeField] private int loseAt = 10;
    [SerializeField] private Checkpoint enemiesSpawn;


    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 5f, 0.5f);  //1s delay, repeat every 1s
    }

    private void Update()
    {
        UpdateGolds();
        UpdateGoldsUi();

        UpdateRenderOrder();
        
        if (loseCounter < loseAt) return;
        
        Time.timeScale = 0;
        Debug.Log("LOSE");
    }

    private static void UpdateRenderOrder()
    {
        var renderers = FindObjectsOfType<SpriteRenderer>();
        foreach (var spriteRenderer in renderers)
        {
            if (spriteRenderer.gameObject.layer == 3 ) continue;
            spriteRenderer.sortingOrder = (int) (spriteRenderer.transform.position.y * -100);
        }
    }

    private void UpdateGolds()
    {
        _elapsedTime += Time.deltaTime;
        // cancel if elapsedTime is not enough
        if (_elapsedTime < goldDonationRate) return;

        _elapsedTime = 0;
        if (golds < maxGolds) AddGolds(donationAmount); // Add golds
        if (golds >= maxGolds) golds = maxGolds; // Clamp to maxGolds
    }

    private void UpdateGoldsUi()
    {
        goldText.text = golds + " / " + maxGolds;
    }
    private void AddGolds(int amount)
    {
        golds += amount;
    }
    
    /// <summary>
    /// Try to make a transaction, returns true for success / false for failure
    /// </summary>
    /// <returns></returns>
    public bool Buy(int price)
    {
        var success = golds - price >= 0;
        golds = success ? golds - price : golds;
        
        return success;
    }

    public void Spawn()
    {
        var enemy = Instantiate(enemyPrefab, enemiesSpawn.transform);
        enemy.GetComponent<Enemy>().targetCheckpoint = enemiesSpawn;
    }
}
