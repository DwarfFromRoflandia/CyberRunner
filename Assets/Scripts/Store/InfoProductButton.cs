using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoProductButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("+");
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("-");
    }
}
