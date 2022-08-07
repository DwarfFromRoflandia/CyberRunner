using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyHearts : MonoBehaviour
{
    private int quantityHearts;
    public int QuantityHearts { get => quantityHearts; }
    private void Start()
    {
        EventManager.BuyHealth.AddListener(AddHearts);
    }

    public void Buy()
    {
        if (EventManager.BuyHealth != null) EventManager.BuyHealth.Invoke();
    }

    public void AddHearts()
    {
        quantityHearts++;
    }

}
