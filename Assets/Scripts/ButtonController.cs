using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonController :MonoBehaviour
{
   public float GameSpeed;
    public GameObject Main_Menu_Condition; // ссылка на главное меню



    public void InputPlay() /* метод при использовании которого запускается игровая сессия 
                              (При нажатии на кнопку Play в главном меню)*/
    {
        Main_Menu_Condition.SetActive(false);
        EventManager.EventPlay?.Invoke(GameSpeed);
    }

     

    
}
