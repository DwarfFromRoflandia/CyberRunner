using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InfoButtonForProductHearts : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator animationBuyButton;
    [SerializeField] private Animator animationInfoText;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("+");

        animationBuyButton.SetTrigger("DisappearingBuyButtonHearts"); //анимация исчезновения кнопки покупки жизней
        animationInfoText.SetTrigger("AppearanceInfoTextForHearts"); //анимация появления текста с информацией о жизнях

    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("-");

        animationBuyButton.SetTrigger("AppearanceBuyButtonHearts"); //анимация повления кнопки покупки жизней
        animationInfoText.SetTrigger("DisappearingInfoTextForHearts"); //анимация текста с информацией о жизнях
    }
}


