using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonController :OpenAndExitStore
{
   public float GameSpeed;
    public GameObject Main_Menu_Condition; // ссылка на главное меню
    public GameObject Settings_Menu;

    
	private void Start()
	{
         
        Settings_Menu.SetActive(false);
	}
	public void InputPlay() /* метод при использовании которого запускается игровая сессия 
                              (При нажатии на кнопку Play в главном меню)*/
    {
        Main_Menu_Condition.SetActive(false);
        EventManager.EventPlay?.Invoke(GameSpeed);
        EventManager.Animation_Play?.Invoke(true);    }

    public void InputSettings()
    {
        Main_Menu_Condition.SetActive(false);
        Settings_Menu.SetActive(true);
    
    
    }

    public  override void ExitMenu()
    {
        Settings_Menu.SetActive(false);
        Main_Menu_Condition.SetActive(true);
    
    }



}
