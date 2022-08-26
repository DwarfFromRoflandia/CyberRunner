using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantityHeartsText : MonoBehaviour
{
    private BuyHearts buyHearts;
    private Text quantityHeartsText;

    private void Start()
    {
        quantityHeartsText = GetComponent<Text>();
        buyHearts = transform.parent.parent.GetComponent<BuyHearts>();
        quantityHeartsText.text =buyHearts.QuantityHearts.ToString();
    }

    private void Update()
    {
        EventManager.BuyHealth.AddListener(HeartsText);
    }

    private void HeartsText()
    {
        quantityHeartsText.text = buyHearts.QuantityHearts.ToString();
    }
}
