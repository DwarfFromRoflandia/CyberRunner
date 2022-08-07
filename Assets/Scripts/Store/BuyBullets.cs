using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBullets : MonoBehaviour
{
    private int quantityBullets;
    public int QuantityBullets { get => quantityBullets; }

    private void Start()
    {
        EventManager.BuyBullets.AddListener(AddBullets);
    }

    public void Buy()
    {
        if (EventManager.BuyBullets != null) EventManager.BuyBullets.Invoke();
    }

    public void AddBullets()
    {
        quantityBullets++;
    }

}
