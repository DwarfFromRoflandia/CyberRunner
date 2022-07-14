using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoProductButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public Text ProductAbout;
    public TextAsset HeartText;
    public TextAsset PatronText;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (name == "InfoButtonHealth")
        {

            ProductAbout.text = HeartText.text;
        }

        else {


            ProductAbout.text = PatronText.text;
        }
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        ProductAbout.text = null;
    }
}
