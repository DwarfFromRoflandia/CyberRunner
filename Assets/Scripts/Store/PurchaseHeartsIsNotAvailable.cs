using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseHeartsIsNotAvailable : MonoBehaviour
{
    private BuyHearts buyHearts;
    private Text text;

    private void Start()
    {
        buyHearts = transform.parent.parent.GetComponent<BuyHearts>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        NoCoinsToBuyHearts();
    }

    private void NoCoinsToBuyHearts()
    {
        if (buyHearts.NumberOfMissingCoins % 10 == 1 && buyHearts.NumberOfMissingCoins != 11)
        {
            text.text = "There is not enough to buy " + buyHearts.NumberOfMissingCoins + " coins.";
        }
        else if ((buyHearts.NumberOfMissingCoins % 10 >= 2 && buyHearts.NumberOfMissingCoins % 10 <= 4) && (buyHearts.NumberOfMissingCoins % 100 != 12 && buyHearts.NumberOfMissingCoins % 100 != 13 && buyHearts.NumberOfMissingCoins % 100 != 14))
        {
            text.text = "There is not enough to buy " + buyHearts.NumberOfMissingCoins + " coins.";
        }
        else
        {
            text.text = "There is not enough to buy" + buyHearts.NumberOfMissingCoins + " coins.";
        }
    }
}
