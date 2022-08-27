using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    private void Start()
	{	   
		playerСoordinates = GetComponent<Transform>();
		EventManager.Animation_Play += Set_Anim_Play_True;
		EventManager.EventPlay?.Invoke(100);
		EventManager.Animation_Play?.Invoke(true);


		//EventManager.GameOverEvent.AddListener(GameOver);

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
		Debug.Log(playerСoordinates.transform.position);
		Player_Anim.SetFloat("Speed", 0);
		EventManager.EventPlay?.Invoke(0);
		gameOverMenu.SetActive(true);
		EventManager.EventPlay += StartRunValues;
		Debug.Log("GameOver");
		StopCoroutine(IncreaseGame());
		isGameOver = true;

	}
	float HealthAfterPunch;
	private void Awake()
	{
		HealthAfterPunch = HealthSlider.value;
		StartCoroutine(IncreaseGame());//увеличиваем постепенно скорость игры
	}

	public void ButtonExitToMainMenu()
	{
	 	 
		SceneManager.LoadScene(0);
		//mainMenu.SetActive(true);
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

			Destroy(other.gameObject, 1f);// удаляем врага через 1 секунды
			Enemy enemy = other.transform.GetComponent<Enemy>();

			StartCoroutine(enemy.Object_Disapear(other.gameObject));//передаем параметр предмета столкновения

			if (other.gameObject.tag != "Gas") // запускаем анимацию спотыкания
			{

				Player_Anim.SetTrigger("Punched");
			}
			   
			if (HealthSlider.value <= 0.1) GameOver(); // проверяем уровень жизни чтобы понять завершать ли игровую сессию

			if (HealthAfterPunch < 0.7 && HealthAfterPunch > 0.3)
			{

				HealthImage.color = Color.yellow;
			
			
			}

			else if (HealthAfterPunch < 0.3f) 
			{

				HealthImage.color = Color.red;

			}

		 



		}
		 
		 
	}

	 
	





	private void OnEnable()
	{
		EventManager.EventPlay += StartRunValues;
		 
		 
		 

	}
	void OnDisable()
	{
		EventManager.EventPlay -= StartRunValues;
		 

	}
	 
	private void FixedUpdate()
	{
		HealthSlider.value = Mathf.MoveTowards(HealthSlider.value, HealthAfterPunch, 0.5f*Time.fixedDeltaTime);// плавное снижене здоровья после удара

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

	 
	private bool paused = true;
    public bool Paused { get => paused;}
	private float BufferPlayerSpeed;
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
}




