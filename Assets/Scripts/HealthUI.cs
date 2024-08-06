using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI playerHealth;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        playerHealth.text = $"Health: {player._health}";
    }
}
