using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private int quantityCoins;


    private void OnEnable()
    {
        if (TransferQuantityCoins.transferQuantityCoins != 0)
        {
            quantityCoins = TransferQuantityCoins.transferQuantityCoins;
        }
    }

    private void OnDestroy()
    {
        TransferQuantityCoins.transferQuantityCoins = quantityCoins;
    }
}
