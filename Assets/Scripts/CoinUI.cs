using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public Player player; // Reference to the player object
    public TextMeshProUGUI totalCoins;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        totalCoins.text = $"Total Coins: {player.totalCoinsCollected}";

    }
}
