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
    }

    private void Update()
    {
        TransferQuantityCoin.transferQuantityCoin = Coins;
    }

    private void AddingCoin()
    { 
        quantityCoins++;
        //TransferQuantityCoin.transferQuantityCoin++;
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
