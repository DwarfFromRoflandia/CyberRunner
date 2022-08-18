using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHearts : MonoBehaviour
{
    [SerializeField] private Button ButtonBuyHearts;
    [SerializeField] private Coin _coin;

    private const int priceHearts = 1;

    private int quantityHearts;
    public int QuantityHearts { get => quantityHearts; }
    private void Start()
    {
        EventManager.BuyHealth.AddListener(AddHearts);
    }

    private void Update()
    {
        if (_coin.Coins < priceHearts) ButtonBuyHearts.enabled = false;//Позже изменить эту строчку на появление текста о недоступности покупки
    }

    public void Buy()
    {
        if (EventManager.BuyHealth != null)
        {
            EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
            EventManager.BuyHealth.Invoke();
        }
    }

    public void AddHearts()
    {
        quantityHearts++;
    }

}
