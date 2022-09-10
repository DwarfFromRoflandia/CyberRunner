using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseBulletsIsNotAvailable : MonoBehaviour
{
    private BuyBullets buyBullets;
    private Text text;
    
    private void Start()
    {
        buyBullets = transform.parent.parent.GetComponent<BuyBullets>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        NoCoinsToBuyBullets();
    }

    private void NoCoinsToBuyBullets()
    {
        if (buyBullets.NumberOfMissingCoins % 10 == 1 && buyBullets.NumberOfMissingCoins != 11)
        {
            text.text = "There is not enough to buy" + buyBullets.NumberOfMissingCoins + "coin.";
        }
        else if ((buyBullets.NumberOfMissingCoins % 10 >= 2 && buyBullets.NumberOfMissingCoins % 10 <= 4) && (buyBullets.NumberOfMissingCoins % 100 != 12 && buyBullets.NumberOfMissingCoins % 100 != 13 && buyBullets.NumberOfMissingCoins % 100 != 14))
        {
            text.text = "There is not enough to buy" + buyBullets.NumberOfMissingCoins + " coin.";
        }
        else
        {
            text.text = "There is not enough to buy " + buyBullets.NumberOfMissingCoins + " coin.";
        }
    }
}
