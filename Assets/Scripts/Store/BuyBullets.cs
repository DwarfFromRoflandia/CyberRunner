using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBullets : MonoBehaviour
{
    [SerializeField] private Text quantityBulletsText;
    private int quantityBullets;


    public void Buy()
    {
        quantityBullets++;
        TransferQuantityBullets.transferQuantityBullets = quantityBullets;
        quantityBulletsText.text = quantityBullets.ToString();
        Debug.Log("Количество патрон: " + TransferQuantityBullets.transferQuantityBullets);
    }

    private void OnEnable()
    {
        if (TransferQuantityBullets.transferQuantityBullets != 0)
        {
            quantityBullets = TransferQuantityBullets.transferQuantityBullets;
        }
    }

    private void OnDestroy()
    {
        TransferQuantityBullets.transferQuantityBullets = quantityBullets;
    }
}
