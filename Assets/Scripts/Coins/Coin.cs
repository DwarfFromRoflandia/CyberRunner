using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private Text textCoin;
    private int coins;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddingCoin()
    {
        coins++;
        textCoin.text = coins.ToString();
    }
}
