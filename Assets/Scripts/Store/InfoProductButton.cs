using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoProductButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject infoText;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("+");
        buyButton.gameObject.SetActive(false);
        infoText.SetActive(true);

    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("-");

        buyButton.gameObject.SetActive(true);
        infoText.SetActive(false);
    }
}
