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
    [SerializeField]
    private List<Material> Time_Cond  = new List<Material>();
    public Sprite MusicOn, MusicOff;

    public Slider MusicSlider;
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

    public void Set_Value_Music()
    {
        PhoneSource.volume = MusicSlider.value;


    }
    static int s=0;
    public void Change_Day_Time( )
	{
         
        switch (s)
        {

            case 0:
                RenderSettings.skybox = Time_Cond[0];
                s = 1;
                break;
            case 1:
                RenderSettings.skybox = Time_Cond[1];
                s = 2;
                break;
            case 2:
                RenderSettings.skybox = Time_Cond[2];
                s = 0;
                break;
            default:
                throw new Exception();
        
        
        
        }
    
    }


}
