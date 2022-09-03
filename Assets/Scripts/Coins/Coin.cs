using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private int quantityCoins;//переменная, которая отвечает за количество монет у игрока
    //public int Coins { get => quantityCoins; }

    public int Coins { get { return quantityCoins; } set { quantityCoins = value; } }


    private void Start()
    {
        EventManager.PickUpCoinEvent.AddListener(AddingCoin);
        
        Coins = PlayerPrefs.GetInt("Coin");
    }

    private void Update()
    {
        TransferQuantityCoin.transferQuantityCoin = Coins;
    }

    private void AddingCoin(GameObject obj)
    {
        if (obj.transform.tag == "Coin")
            quantityCoins++;
         
        else
            quantityCoins += 10;
      

        PlayerPrefs.SetInt("Coin", quantityCoins);
    }

    private void OnEnable()
    {
        if (TransferQuantityCoin.transferQuantityCoin != 0)
        {
            Coins = TransferQuantityCoin.transferQuantityCoin;
        }
    }

    private void OnDestroy()
    {
        TransferQuantityCoin.transferQuantityCoin = Coins;
    }


}
