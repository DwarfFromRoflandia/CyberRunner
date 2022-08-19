using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyBullets : MonoBehaviour
{
    [SerializeField] private Text textTheUnavailabilityPurchaseBullets;
    [SerializeField] private Button buttonBuyBullets;
    [SerializeField] private Coin _coin;
    
    private int quantityBullets;
    public int QuantityBullets { get => quantityBullets; }
   
    private const int priceBullets = 10; //10


    private int numberOfMissingCoins; //переменная, которая отвечает за количество недостающих монет для покупки патрон
    public int NumberOfMissingCoins { get => numberOfMissingCoins;}
    private void Start()
    {
        EventManager.BuyBullets.AddListener(AddBullets);
        numberOfMissingCoins = priceBullets - _coin.Coins;
    }

    private void Update()
    {
        if (_coin.Coins < priceBullets)
        {
            //buttonBuyBullets.enabled = false;//Позже изменить эту строчку на появление текста о недоступности покупки
            buttonBuyBullets.gameObject.SetActive(false);
            textTheUnavailabilityPurchaseBullets.gameObject.SetActive(true);
        }     
    }

    public void Buy()
    {
        
        if (EventManager.BuyBullets != null)
        {
            EventManager.BuyBullets.Invoke();
            EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
        }
    }

    public void AddBullets()
    {
        quantityBullets++;
        _coin.Coins -= priceBullets;
    }

}
