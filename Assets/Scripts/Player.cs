using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	

	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject secondStartPoint;
	private Transform playerСoordinates;
	private float speed;
	[SerializeField] private float distanceGravit = 7;
	[SerializeField] private Image HealthImage;
 

	public float PlayerSpeed;

	[SerializeField] private ShotGun shot;
	public Transform StartPoint;
	public Animator Player_Anim;
	public float TimeBeReady = 3;
	[SerializeField] private SpawnManager spawnManager;
	private RaycastHit hit;
	

	public Text TextTimeToStart;
	[SerializeField] private Slider HealthSlider;
	 

	[SerializeField] private Sprite Play, Stop;
	[SerializeField] private ParticleSystem ParticleInCoin;
	[HideInInspector] public Rigidbody rb;
	public Image PauseImage;

	private bool isGameOver;
    public bool IsGameOver { get => isGameOver;}

	private bool isPauseOn = false;

    public bool IsPauseOn { get => isPauseOn;}

	public AdvertistGoing SliderAdver;

	[SerializeField] private GameObject ButtonHealth;
	private Text TextHealth;

	private bool paused = true;
	public bool Paused { get => paused; }

	private float BufferPlayerSpeed;

	private ParticleSystem ParticleReBorn;

	[SerializeField] private GameObject ClicPlatform;
	private void Start()
	{	   
		playerСoordinates = GetComponent<Transform>();
		 
		EventManager.EventPlay?.Invoke(100);
		
		EventManager.Animation_Play?.Invoke(true);

		Player_Anim.SetBool("Die", false);

		TextHealth = ButtonHealth.transform.GetChild(0).GetComponent<Text>();

		if (TextHealth.text == "0")
			ButtonHealth.GetComponent<Button>().interactable = false;

		TextHealth.text = PlayerPrefs.GetInt("Heart").ToString();

		isGameOver = false;

		

	}
	 
    public void False()//метод добавляет Rigidbody после анимации поворота и передает скорость в аниматор
	{
		Player_Anim.SetBool("PlayIsPressed", false);
		Player_Anim.SetFloat("Speed", 40);

		gameObject.AddComponent<Rigidbody>();
		 
		rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;
		Player_Anim.applyRootMotion = false; //замораживаем повороты и перемещения анимаций после поворота нашей первой анимации
		
		 
	}
	 
	public void GameOver()//метод, отвечающий за конец игры
	{
		ClicPlatform.SetActive(false);

		Player_Anim.SetFloat("Speed", 0);

		

		if (PlayerSpeed > 0) // так как колайдер вывзывается несколько раз - BufferSpeed Обнуляется
		{
			BufferPlayerSpeed = PlayerSpeed;
		}

		PlayerSpeed = 0;

		EventManager.SetSpeedCar?.Invoke(0);

		

		StopAllCoroutines();//останавливаем увеличичение  скорости игры

	

		isGameOver = true;

		rb.AddForce(Vector3.forward * -1 * Time.deltaTime * 5_000);




	




	}
	public void UseHealth()// увеличиваем жизнь за счет купленных сердечек
	{
		Button ButtonHeart = ButtonHealth.GetComponent<Button>();

		int heart = PlayerPrefs.GetInt("Heart");

		EventManager.ButtonClicked.Invoke();

		if (heart > 0)
		{
		 

			ButtonHeart.interactable = true;

			PlayerPrefs.SetInt("Heart", heart - 1);

			TextHealth.text = heart.ToString();

			HealthAfterPunch += 0.5f;

			CheckColorHealth();

		}

		else //если жизни закончились - деактивируем кнопочку
		{
		TextHealth.text = "0";

		ButtonHeart.interactable = false;
		
		}
	
	}

	public void Velocity_Null()
	{
		 

	 

		gameOverMenu.SetActive(true);

	 


		StartCoroutine(SliderAdver.IncreaseSlider());



	}
	float HealthAfterPunch;
	 
	private void Awake()
	{
		HealthAfterPunch = HealthSlider.value;
		StartCoroutine(IncreaseGame());//увеличиваем постепенно скорость игры
		Time.timeScale = 1;

		EventManager.AdvertisIsShowed.AddListener(RewardPlayer);// подписка на награду за рекламу

	}

	public void ButtonExitToMainMenu()
	{

		SceneManager.LoadScene(0);
		gameOverMenu.SetActive(false);
		playerСoordinates.transform.position = secondStartPoint.transform.position;
		HealthSlider.value = 1;

		DontDestroyOnLoad(gameObject); /* юнити ссылается на события из скрипта которые в выключенном состоянии во время добавления новой сцены.
		                                * Поэтому мы не удаляем игрока а переносим его на новую сцену чтобы не возникало ошибок.Однако в новой сцене у нас будет 2 персонажа одновременно.
		                              * Поэтому удаляем лишнего из них*/
		Destroy(gameObject, 0.1f);
	
	}



	private void OnCollisionExit(Collision other)
	{
		if (other.transform.tag == "MetalObstacle" || other.transform.tag == "Car" || other.transform.tag == "Obstacle")
		{
			speed = 0;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
           
        }
    }
    private void OnCollisionEnter(Collision other)
    {
		if (other.transform.tag == "Coin"||other.transform.tag == "GoldCoin")
			
        { 
            Instantiate(ParticleInCoin, other.transform.position + new Vector3(0, 8f, 0), other.transform.rotation);//при соприкосновении коллайдера игрока с монеткой появляется дымка от исчезнувшей монеты
			Destroy(other.gameObject);
            if (EventManager.PickUpCoinEvent != null) EventManager.PickUpCoinEvent.Invoke(other.gameObject);
			if (EventManager.AudioCoinEvent != null) EventManager.AudioCoinEvent.Invoke();
        }
		 

		if (other.transform.tag == "MetalObstacle" || other.transform.tag == "Car"|| other.transform.tag == "Obstacle"||other.transform.tag=="Gas")
		{
			 
			HealthAfterPunch = HealthSlider.value - EventManager.IsPunched.Invoke(0);// меняем значение здоровья игрока вызывая событие

			HealthImage = HealthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();

			Destroy(other.gameObject, 0.6f);// удаляем врага через 0.6 секунды

			Enemy enemy = other.transform.GetComponent<Enemy>();

		

			StartCoroutine(enemy.Object_Disapear(other.gameObject));//передаем параметр предмета столкновения

			if (other.gameObject.tag != "Gas") // запускаем анимацию спотыкания
			{

				Player_Anim.SetTrigger("Punched");
			}

			if (HealthSlider.value <= 0.1) // проверяем уровень жизни чтобы понять завершать ли игровую сессию
			{
				 

				Player_Anim.SetTrigger("Die");

				 

				GameOver();// переключаем на анимацию смерти  
				
			}


			CheckColorHealth();
		 



		}
		 
		 
	}

	 
	





	private void OnEnable()
	{
		EventManager.EventPlay += StartRunValues;

		EventManager.Animation_Play += Set_Anim_Play_True;

		


	}
	void OnDisable()
	{
		EventManager.EventPlay -= StartRunValues;

		EventManager.Animation_Play -= Set_Anim_Play_True;

		
	}
	 
	private void FixedUpdate()
	{

		print(BufferPlayerSpeed);
		HealthSlider.value = Mathf.MoveTowards(HealthSlider.value, HealthAfterPunch, 0.3f*Time.fixedDeltaTime);// плавное снижене здоровья после удара

		StartRunValues(PlayerSpeed);

		Physics.Raycast(transform.position, Vector3.down, out hit); //создаем луч для проверки

		if (hit.distance > distanceGravit && (hit.transform.tag.Equals("Road") || hit.transform.tag.Equals("Obstacle"))
			&& rb != null) // если мы прыгнули выше высоты
								  // прыжка и под нами дорога или препятствие - тогда падаем
		{


			rb.velocity = Vector3.zero;
			rb.velocity = -transform.up * Time.deltaTime * 15_000;



		}
		else if (rb != null && hit.distance < distanceGravit) // если мы на земле
		{

			rb.velocity = new Vector3(0, 0, speed);// обнуляем ускорение

		}
	}

	private void StartRunValues(float GameSpeed)
	{
		
 
		transform.Translate(0, 0, GameSpeed * Time.deltaTime, Space.World);

	 
	}

	 
	 
    public void Pause()
	{

		EventManager.ButtonClicked.Invoke();//звук
		if (paused)
		{
			BufferPlayerSpeed = PlayerSpeed;//сохраняем значение скорости
			PlayerSpeed = 0;//придаем ноль сколости
			Player_Anim.SetFloat("Speed",0);
			paused = false;
			PauseImage.sprite = Stop;
			StopAllCoroutines();//останавливаем ускорение игры
			isPauseOn = true;
			EventManager.SetSpeedCar.Invoke(0f);// останавливаем машины

				}
		else
		{


			StartCoroutine(PauseReset());

			TextTimeToStart.gameObject.SetActive(true);

			
			PauseImage.sprite = Play;

			 

			paused = true;
			

		}

	}
 


	public void Include_to_Run()
	{
		Player_Anim.SetBool("MoveRight", false);
		Player_Anim.SetBool("MoveLeft", false);
		 
	}

	IEnumerator PauseReset()
	{
		float TimeStart = Time.time;
		float TimeToGo = Time.time + TimeBeReady;
		while (TimeStart < TimeToGo) //3 секунды до старта игры
		{
			TimeStart = Time.time;
			TextTimeToStart.text = (TimeToGo - TimeStart).ToString();


			yield return null;
			
		}
	


		TextTimeToStart.gameObject.SetActive(false) ;
		StopCoroutine(PauseReset());

		PlayerSpeed = BufferPlayerSpeed;//снова придаем телу данную ему скорость 
		Player_Anim.SetFloat("Speed", 50);

		isPauseOn = false;
		StartCoroutine(IncreaseGame());

		EventManager.SetSpeedCar.Invoke(6000f);//обратно придаем скорость машинам

	}

	private IEnumerator IncreaseGame()//метод отвечающий за постепенное увеличение скорости игры
	{

		while (true)
		{

			yield return new WaitForSeconds(5f);
			PlayerSpeed += 5;

		}
	}
	

	private void Set_Anim_Play_True(bool b)
	{

		Player_Anim.SetBool("PlayIsPressed", b);

	}

	
	 
	 
	 
	public void StartShot()
	{
		shot.Shot();
	}

	

	public void OnDrag(PointerEventData eventData)
	{
		throw new NotImplementedException();
	}
	void Get_Speed()


		=> speed = 300;



	void ResetSpeed()

		=> speed = 0;

	private void CheckColorHealth()
	{
		if (HealthAfterPunch < 0.7 && HealthAfterPunch > 0.3)  HealthImage.color = Color.yellow;

		else if (HealthAfterPunch < 0.3f)					   HealthImage.color = Color.red;

		else if (HealthAfterPunch >= 0.9f)					   HealthImage.color = Color.green;
		
		
	}

	public void RewardPlayer()
	{

		Time.timeScale = 1;

		ClicPlatform.SetActive(true);

		PlayerSpeed = BufferPlayerSpeed;

		ParticleReBorn = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();

		ParticleReBorn.Play();

		Player_Anim.SetTrigger("ReBorn");

		Player_Anim.SetFloat("Speed",PlayerSpeed);

		EventManager.SetSpeedCar?.Invoke(6000);

		HealthAfterPunch = 1;

		StartCoroutine(IncreaseGame());//останавливаем увеличичение  скорости игры

		CheckColorHealth();

		gameOverMenu.SetActive(false);

		isGameOver = false;

		


	}
}




