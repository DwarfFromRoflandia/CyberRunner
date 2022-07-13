using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoButtonForProductBullets : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    [SerializeField] private Animator animationBuyButton;
    [SerializeField] private Animator animationInfoText;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("+");

        animationBuyButton.SetTrigger("DisappearingBuyButtonBullets"); //анимация исчезновения кнопки покупки патрон
        animationInfoText.SetTrigger("AppearanceInfoTextForBullets"); //анимация появления текста с информацией о патронах
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("-");

        animationBuyButton.SetTrigger("AppearanceBuyButtonBullets"); //анимация повления кнопки покупки патрон
        animationInfoText.SetTrigger("DisappearingInfoTextForBullets"); //анимация текста с информацией о патронах
    }
}
