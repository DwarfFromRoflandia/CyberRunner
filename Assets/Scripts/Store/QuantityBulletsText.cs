using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantityBulletsText : MonoBehaviour
{
    private BuyBullets buyBullets;
    private Text quantityBulletsText;

    private void Start()
    {
        quantityBulletsText = GetComponent<Text>();
        buyBullets = transform.parent.parent.GetComponent<BuyBullets>();
    }

    private void Update()
    {
        EventManager.BuyBullets.AddListener(BulletsText);
    }

    private void BulletsText()
    {
        quantityBulletsText.text = buyBullets.QuantityBullets.ToString();
    }
}
