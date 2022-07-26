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
    public Text TextWarnings;// предупреждение текста для ввода имени
    private AudioSource PhoneSource;
    private Image MusicImage;
    [SerializeField]
    private List<Material> Time_Cond  = new List<Material>();

    [SerializeField]
    private List<Sprite> Time_Sprites = new List<Sprite>(3);
    private Image Image_Time_Now;
    
    public Sprite MusicOn, MusicOff;

    public Slider MusicSlider;

    public GameObject Change_Name_Button;
    private   InputField  Input_Field;

    private Animation Animation_Disapearing;

    Animation Input_Field_Anim;
    Transform Input_Field_Trans;

	 
	private void Start()
	{

        Image_Time_Now = gameObject.transform.GetChild(3).GetChild(4).GetComponent<Image>();
        PhoneSource = GameObject.Find("MainMenuCanvas").GetComponent<AudioSource>();
        MusicImage = GameObject.Find("ButtonMusic").GetComponent<Image>();
        Settings_Menu.SetActive(false);
        PlayerPrefs.SetString("MusicCondition","On");
        Music();

        Animation_Disapearing = Change_Name_Button.GetComponent<Animation>();

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
        Input_Field_Trans = gameObject.transform.GetChild(2).GetChild(0).transform.GetComponent<Transform>();
        Input_Field = Input_Field_Trans.GetComponent<InputField>(); // инициализируем переменные перед переходом в настройки
        InputName();




    }

	public  override void ExitMenu()
    {
        Settings_Menu.SetActive(false);
        Main_Menu_Condition.SetActive(true);
        Change_Name_Button.GetComponent<Image>().color = new Color(67, 180, 170,255);
        
        Input_Field.gameObject.SetActive(false);
        TextWarnings.text = "";
        Change_Name_Button.gameObject.SetActive(true);
        


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

    public void Vk()=>Application.OpenURL("https://vk.com/id446930815");

     
   
    public void Button_Change_Name_Down()
    {

 
         
        Animation_Disapearing.Play();

        

        Input_Field_Trans.gameObject.SetActive(true);

        Input_Field_Anim = Input_Field_Trans.GetComponent<Animation>();
  
        Input_Field_Anim.Play();
        


    
    }




     
    public void InputName()
    {
      


        if (Input_Field.text.Length > 17|| Input_Field.text.Length < 4)
        {
            TextWarnings.color = Color.red;
      
            TextWarnings.text = "У кибервоина слишком длинное или короткое имя.\n Не больше 17 и не меньше 4 знаков,Измените его!";
            TextWarnings.gameObject.SetActive(true);
            



        }

       
		else
		{

            print("Все ровно!");
            TextWarnings.color = Color.green;
            TextWarnings.text = "Имя подходит!";
            EventManager.ChangeNameEvent?.Invoke(Input_Field.text);


        }
        Animation Anim_Text = TextWarnings.GetComponent<Animation>();
        Anim_Text.Play();


       
    }

 


}
