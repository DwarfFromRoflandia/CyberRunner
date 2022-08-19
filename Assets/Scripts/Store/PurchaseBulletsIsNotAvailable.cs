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
        text.text = "Для покупки не хватает " + buyBullets.NumberOfMissingCoins + " монет.";
    }
}
