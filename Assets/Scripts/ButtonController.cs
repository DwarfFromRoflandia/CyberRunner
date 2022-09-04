using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController :OpenAndExitStore
{
    [Range(100,300)]
    public float GameSpeed=100; // задаем ограничения скорости

    public GameObject Main_Menu_Condition; // ссылка на главное меню
    public GameObject Settings_Menu;
    public GameObject Change_Name_Button;

    public Text TextWarnings;// предупреждение текста для ввода имени

    private AudioSource PhoneSource;

    private Image MusicImage;

    [SerializeField]
    private List<Material> Time_Cond  = new List<Material>();
    [SerializeField]
    private List<Sprite> Time_Sprites = new List<Sprite>(3);
    [SerializeField]

    private Image Image_Time_Now;
    
    public Sprite MusicOn, MusicOff;
    public Slider MusicSlider;

  

    private   InputField  Input_Field;

    private Animation Animation_Disapearing;

    public static bool MusicIsPressed = true; 

    Animation Input_Field_Anim;
    Transform Input_Field_Trans;
    static Material Time_Now;
    public Canvas PersonalCanvas;
    public bool PauseIsPressed;
    [SerializeField] private Image PauseImage;
    [SerializeField] private Light Light;
    public TouchScreenKeyboard keyboard;
    private void Start()
	{
       
    
        PhoneSource = GameObject.Find("MainMenuCanvas").GetComponent<AudioSource>();
        
        MusicImage = GameObject.Find("ButtonMusic").GetComponent<Image>();

        Music();
       



        Animation_Disapearing = Change_Name_Button?.GetComponent<Animation>();
      

       if(SceneManager.GetActiveScene().buildIndex==1&& Time_Now!=null) // если присутствует скайбокс и мы в сцене игры
                                                                        // - тогда прнидаем скорость, разворачиваемся и меняем фон на заданный
        RenderSettings.skybox = Time_Now;

       
        EventManager.EventPlay?.Invoke(GameSpeed);
        EventManager.Animation_Play?.Invoke(true);
      


    }
 


	public void InputPlay() /* метод при использовании которого запускается игровая сессия 
                              (При нажатии на кнопку Play в главном меню)*/
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
        SceneManager.LoadScene(1);
        EventManager.Animation_Play?.Invoke(true);
    
  


    


}

    public void InputSettings()
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
        Main_Menu_Condition.SetActive(false);
        Settings_Menu.SetActive(true);
        Input_Field_Trans = gameObject.transform.GetChild(4).GetChild(0).transform.GetComponent<Transform>();
        Input_Field = Input_Field_Trans.GetComponent<InputField>(); // инициализируем переменные перед переходом в настройки
        Input_Field.text = PlayerPrefs.GetString("Name");
        InputName();




    }
  
	public  override void ExitMenu()
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
        Settings_Menu.SetActive(false);
        Main_Menu_Condition.SetActive(true);
        Change_Name_Button.GetComponent<Image>().color = new Color(67, 180, 170,255);
        
        Input_Field.gameObject.SetActive(false);
        TextWarnings.text = "";
        Change_Name_Button.gameObject.SetActive(true);

        



    }
 
   
 
    


    public void Music( )
    {

        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 

        if (MusicIsPressed)
        {

            PhoneSource.Play();
            MusicIsPressed = false;
            MusicImage.sprite = MusicOn;


        }

        else if(!MusicIsPressed)
         {

            PhoneSource.Stop();
            MusicIsPressed = true;
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
       
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
         
        switch (s)
        {
             
            case "Day":
                s = "Evening";
                Image_Time_Now.sprite = Time_Sprites[0];
                Time_Now = Time_Cond[0];
           
              
                break;

            case "Evening":
                s = "Night";
                Image_Time_Now.sprite = Time_Sprites[1];
                Time_Now = Time_Cond[1];
 
                break;

            case "Night":
                s = "Day";
                Image_Time_Now.sprite = Time_Sprites[2];
                Time_Now = Time_Cond[2];
           
      

                break;

            default:
                print("ничего не выбрано");
                break;
                 

        }
      
     
        RenderSettings.skybox = Time_Now;
    }



    public void Vk()
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
        Application.OpenURL("https://vk.com/id446930815");
        Application.OpenURL("https://vk.com/dwarffromroflandia");
    }



        public void Button_Change_Name_Down()
    {

        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 

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

          
            TextWarnings.color = Color.green;
            TextWarnings.text = "Имя подходит!";
            PlayerPrefs.SetString("Name", Input_Field.text);// фиксируем в базе измененное имя



        }
        Animation Anim_Text = TextWarnings.GetComponent<Animation>();
        Anim_Text.Play();

       
       
    }

 


}
