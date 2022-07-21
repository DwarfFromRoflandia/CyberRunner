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

    [SerializeField]
    private List<Sprite> Time_Sprites = new List<Sprite>(3);
    private Image Image_Time_Now;
    
    public Sprite MusicOn, MusicOff;

    public Slider MusicSlider;
	private void Start()
	{
        Image_Time_Now = gameObject.transform.GetChild(3).GetChild(4).GetComponent<Image>();
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
    static string s="Day";
    public void Change_Day_Time( )
	{
        Material Time_Now;
         
        switch (s)
        {

            case "Day":
                Time_Now = Time_Cond[0];
                s = "Evening";
                Image_Time_Now.sprite = Time_Sprites[0];
                break;

            case "Evening":
                Time_Now = Time_Cond[1];
                s = "Night";
                Image_Time_Now.sprite = Time_Sprites[1];
                break;

            case "Night":
                Time_Now = Time_Cond[2];
                s = "Day";
                Image_Time_Now.sprite = Time_Sprites[2];
                break;

            default:
                throw new Exception();

           



        }
        RenderSettings.skybox = Time_Now;
    }


}
