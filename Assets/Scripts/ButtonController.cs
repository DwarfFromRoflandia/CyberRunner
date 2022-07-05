using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonController :MonoBehaviour
{
   public float GameSpeed;
    public GameObject Main_Menu_Condition; // ссылка на главное меню
    public Animation Menu_Settings; // ссылка на меню настроек


    public void InputPlay() /* метод при использовании которого запускаетс€ игрова€ сесси€ 
                              (ѕри нажатии на кнопку Play в главном меню)*/
    {
        Main_Menu_Condition.SetActive(false);
        EventManager.EventPlay?.Invoke(GameSpeed);
    }

    public void InputSettings()
	{
        Main_Menu_Condition.SetActive(false); 
        Menu_Settings.Play();

    }

   public void ExitSettings()
    {
        RectTransform size = GameObject.Find("ButtonExit").GetComponent<RectTransform>();
        if (Input.GetMouseButtonDown(0))
        {

           //если зажали кнопку - то что то делаем
        
        }

        if (Input.GetMouseButtonUp(0))
        { 
        // если разжали кнопку то что-то делаем
        }
    
    }
     

    
}
