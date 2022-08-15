using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCoin : MonoBehaviour
{
    private Text textQuantityCoin;
    private Coin _coin;

    private void Start()
    {
        textQuantityCoin = GetComponent<Text>();
        _coin = transform.parent.GetComponent<Coin>();

        EventManager.PickUpCoinEvent.AddListener(QuantityCoinText);
    }

    private void QuantityCoinText()
    {
        textQuantityCoin.text = TransferQuantityCoin.transferQuantityCoin.ToString();
    }
}
