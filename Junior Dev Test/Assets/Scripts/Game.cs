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

    private void Update()
    {
        UpdateGolds();
        UpdateGoldsUi();
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
}
