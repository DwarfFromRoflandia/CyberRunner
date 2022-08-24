using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private MeterCounter _meterCounter;
    [SerializeField] private Coin _coin;

    [SerializeField] private Text scoreMeter;
    [SerializeField] private Text coinCount;
    
    private Player player;

    private void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }

    private void Update()
    {
        GameOverMenu();

        scoreMeter.text = _meterCounter.MeterCount + " m";
        coinCount.text = _coin.Coins.ToString();
    }
    private void GameOverMenu()
    {
        player.GameOver();
    }

   

}
