using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyBullets : MonoBehaviour
{
    [SerializeField] private Button buttonBuyBullets;
    [SerializeField] private Coin _coin;
    
    private int quantityBullets;
    public int QuantityBullets { get => quantityBullets; }
   
    private const int priceBullets = 1;//50

    private void Start()
    {
        EventManager.BuyBullets.AddListener(AddBullets);
    }

    private void Update()
    {
        if (_coin.Coins < priceBullets) buttonBuyBullets.enabled = false;//Позже изменить эту строчку на появление текста о недоступности покупки
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
    }

}
