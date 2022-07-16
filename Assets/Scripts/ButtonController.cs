using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ButtonController :OpenAndExitStore
{
   public float GameSpeed;
    public GameObject Main_Menu_Condition; // ссылка на главное меню
    public GameObject Settings_Menu;

    private AudioSource PhoneSource;
    private Image MusicImage;

    public Sprite MusicOn, MusicOff;
	private void Start()
	{
        PhoneSource = GameObject.Find("MainMenuCanvas").GetComponent<AudioSource>();
        MusicImage = GameObject.Find("ButtonMusic").GetComponent<Image>();
        Settings_Menu.SetActive(false);
        PlayerPrefs.SetString("MusicCondition","On");
        Music();
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

    public void Music()
    {
        if (PlayerPrefs.GetString("MusicCondition") == "On")
        {

            PhoneSource.Play();
            PlayerPrefs.SetString("MusicCondition", "Off");
            MusicImage.sprite = MusicOn;


        }

        else if(PlayerPrefs.GetString("MusicCondition") =="Off")
         {

            PhoneSource.Stop();
            PlayerPrefs.SetString("MusicCondition", "On");
            MusicImage.sprite = MusicOff;
        }
    
    
    
    }


}
