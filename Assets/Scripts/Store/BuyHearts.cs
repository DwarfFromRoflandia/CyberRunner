using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHearts : MonoBehaviour
{
    [SerializeField] private Text quantityHeartsText;
   
    private int quantityHearts;
    private const int priseForHearts = 200;
    
    public void Buy()
    {
        quantityHearts++;
        TransferQuantityHearts.transferQuantityHearts = quantityHearts;
        quantityHeartsText.text = quantityHearts.ToString();
        Debug.Log("Количество сердец: " + TransferQuantityHearts.transferQuantityHearts);
    }
    
    private void OnEnable()
    {
        if (TransferQuantityHearts.transferQuantityHearts != 0)
        {
            quantityHearts = TransferQuantityHearts.transferQuantityHearts;
        }
    }
    private void OnDestroy()
    {
        TransferQuantityHearts.transferQuantityHearts = quantityHearts; 
    }
}
