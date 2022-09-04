using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHearts : MonoBehaviour
{
    [SerializeField] private Text textTheUnavailabilityPurchaseHearts;
    [SerializeField] private Button ButtonBuyHearts;
    [SerializeField] private Coin _coin;


    private const int priceHearts = 100; //100

    private int quantityHearts;
    public int QuantityHearts { get => quantityHearts; }

    private int numberOfMissingCoins;//переменная, которая отвечает за количество недостающих монет для покупки сердец
    public int NumberOfMissingCoins { get => numberOfMissingCoins; }

    private bool isThePurchaseHeartsAvailable = true;
    public bool IsThePurchaseHeartsAvailable { get => isThePurchaseHeartsAvailable; }
    private void Start()
    {
        quantityHearts = PlayerPrefs.GetInt("Heart");
        quantityHearts = 10;
        EventManager.BuyHealth.AddListener(AddHearts);
    }

    private void Update()
    {
        if (_coin.Coins < priceHearts)
        {
            //ButtonBuyHearts.enabled = false;//Позже изменить эту строчку на появление текста о недоступности покупки
            ButtonBuyHearts.gameObject.SetActive(false);
            textTheUnavailabilityPurchaseHearts.gameObject.SetActive(true);
            isThePurchaseHeartsAvailable = false;
        }

        numberOfMissingCoins = priceHearts - _coin.Coins;
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
        PlayerPrefs.SetInt("Heart", quantityHearts);
        _coin.Coins -= priceHearts;
        PlayerPrefs.SetInt("Coin", _coin.Coins);
    }

}
