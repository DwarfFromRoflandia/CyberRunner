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
	[SerializeField] private MeterCounter meterCounter;
	private Transform player�oordinates;
	private float speed;
	[SerializeField] private float distanceGravit = 7;
	[SerializeField] private Image HealthImage;
 
	[Range(100,400)]
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
		player�oordinates = GetComponent<Transform>();
		 
		EventManager.EventPlay?.Invoke(100);
		
		EventManager.Animation_Play?.Invoke(true);

		Player_Anim.SetBool("Die", false);

		TextHealth = ButtonHealth.transform.GetChild(0).GetComponent<Text>();

		if (TextHealth.text == "0")
			ButtonHealth.GetComponent<Button>().interactable = false;

		TextHealth.text = PlayerPrefs.GetInt("Heart").ToString();

		isGameOver = false;

		

	}
	 
    public void False()//����� ��������� Rigidbody ����� �������� �������� � �������� �������� � ��������
	{
		Player_Anim.SetBool("PlayIsPressed", false);
		Player_Anim.SetFloat("Speed", 40);

		gameObject.AddComponent<Rigidbody>();
		 
		rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;
		Player_Anim.applyRootMotion = false; //������������ �������� � ����������� �������� ����� �������� ����� ������ ��������
		
		 
	}
	 
	public void GameOver()//�����, ���������� �� ����� ����
	{
		ClicPlatform.SetActive(false);

		Player_Anim.SetFloat("Speed", 0);

		

		if (PlayerSpeed > 0) // ��� ��� �������� ����������� ��������� ��� - BufferSpeed ����������
		{
			BufferPlayerSpeed = PlayerSpeed;
		}

		PlayerSpeed = 0;

		EventManager.SetSpeedCar?.Invoke(0);

		

		StopAllCoroutines();//������������� ������������  �������� ����

	

		isGameOver = true;

		rb.AddForce(Vector3.forward * -1 * Time.deltaTime * 5_000);




	




	}
	public void UseHealth()// ����������� ����� �� ���� ��������� ��������
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

		else //���� ����� ����������� - ������������ ��������
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
		StartCoroutine(IncreaseGame());//����������� ���������� �������� ����

		StartCoroutine(meterCounter.MeterCounterCoroutine());//��������� �������� ������      
		StartCoroutine(meterCounter.MeterCounterSpeedCoroutine());//����������� ���������� �������� ������
		

		Time.timeScale = 1;

		EventManager.AdvertisIsShowed.AddListener(RewardPlayer);// �������� �� ������� �� �������

	}

	public void ButtonExitToMainMenu()
	{

		SceneManager.LoadScene(0);
		gameOverMenu.SetActive(false);
		player�oordinates.transform.position = secondStartPoint.transform.position;
		HealthSlider.value = 1;

		DontDestroyOnLoad(gameObject); /* ����� ��������� �� ������� �� ������� ������� � ����������� ��������� �� ����� ���������� ����� �����.
		                                * ������� �� �� ������� ������ � ��������� ��� �� ����� ����� ����� �� ��������� ������.������ � ����� ����� � ��� ����� 2 ��������� ������������.
		                              * ������� ������� ������� �� ���*/
		Destroy(gameObject, 0.1f);
	
	}



	private void OnCollisionExit(Collision other)
	{
		if (other.transform.tag == "MetalObstacle" || other.transform.tag == "Car" || other.transform.tag == "Obstacle")
		{
			speed = 0;
		}
	}
	 
	
    private void OnCollisionEnter(Collision other)
    {
		if (other.transform.tag == "Coin"||other.transform.tag == "GoldCoin")
			
        { 
            Instantiate(ParticleInCoin, other.transform.position + new Vector3(0, 8f, 0), other.transform.rotation);//��� ��������������� ���������� ������ � �������� ���������� ����� �� ����������� ������
			Destroy(other.gameObject);
            if (EventManager.PickUpCoinEvent != null) EventManager.PickUpCoinEvent.Invoke(other.gameObject);
			if (EventManager.AudioCoinEvent != null) EventManager.AudioCoinEvent.Invoke();
        }
		 

		if (other.transform.tag == "MetalObstacle" || other.transform.tag == "Car"|| other.transform.tag == "Obstacle"||other.transform.tag=="Gas")
		{
			 
			HealthAfterPunch = HealthSlider.value - EventManager.IsPunched.Invoke();// ������ �������� �������� ������ ������� �������

			HealthImage = HealthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();

			Destroy(other.gameObject, 0.6f);// ������� ����� ����� 0.6 �������

			Enemy enemy = other.transform.GetComponent<Enemy>();

		

			StartCoroutine(enemy?.Object_Disapear(other.gameObject));//�������� �������� �������� ������������

			if (other.gameObject.tag != "Gas") // ��������� �������� ����������
			{

				Player_Anim.SetTrigger("Punched");
			}

			if (HealthSlider.value <= 0.1) // ��������� ������� ����� ����� ������ ��������� �� ������� ������
			{
				 

				Player_Anim.SetTrigger("Die");

				 

				GameOver();// ����������� �� �������� ������  
				
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

	 
		HealthSlider.value = Mathf.MoveTowards(HealthSlider.value, HealthAfterPunch, 0.6f*Time.fixedDeltaTime);// ������� ������� �������� ����� �����

		StartRunValues(PlayerSpeed);

		Physics.Raycast(transform.position, Vector3.down, out hit); //������� ��� ��� ��������

		if (hit.distance > distanceGravit && (hit.transform.tag.Equals("Road") || hit.transform.tag.Equals("Obstacle"))
			&& rb != null) // ���� �� �������� ���� ������
						   // ������ � ��� ���� ������ ��� ����������� - ����� ������
		{


			rb.velocity = Vector3.zero;
			rb.velocity = -transform.up * Time.deltaTime * 15_000;



		}
		else if (rb != null && hit.distance < distanceGravit) // ���� �� �� �����
		{

			rb.velocity = new Vector3(0, 0, speed);// �������� ���������

		}
	}

	private void StartRunValues(float GameSpeed)
	{
		
 
		transform.Translate(0, 0, GameSpeed * Time.deltaTime, Space.World);

	 
	}

	 
	 
    public void Pause()
	{

		EventManager.ButtonClicked.Invoke();//����
		if (paused)
		{
			BufferPlayerSpeed = PlayerSpeed;//��������� �������� ��������
			PlayerSpeed = 0;//������� ���� ��������
			Player_Anim.SetFloat("Speed",0);
			paused = false;
			PauseImage.sprite = Stop;
			StopAllCoroutines();//������������� ��������� ����
			isPauseOn = true;
			EventManager.SetSpeedCar.Invoke(0f);// ������������� ������

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
		while (TimeStart < TimeToGo) //3 ������� �� ������ ����
		{
			TimeStart = Time.time;
			TextTimeToStart.text = (TimeToGo - TimeStart).ToString();


			yield return null;
			
		}
	


		TextTimeToStart.gameObject.SetActive(false) ;
		StopCoroutine(PauseReset());

		PlayerSpeed = BufferPlayerSpeed;//����� ������� ���� ������ ��� �������� 
		Player_Anim.SetFloat("Speed", 50);

		isPauseOn = false;
		StartCoroutine(IncreaseGame());

		StartCoroutine(meterCounter.MeterCounterCoroutine());//��������� �������� ������
		StartCoroutine(meterCounter.MeterCounterSpeedCoroutine());//����������� ���������� �������� ������



		EventManager.SetSpeedCar.Invoke(6000f);//������� ������� �������� �������

	}

	private IEnumerator IncreaseGame()//����� ���������� �� ����������� ���������� �������� ����
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

		StartCoroutine(IncreaseGame());//������������� ������������  �������� ����

		CheckColorHealth();

		gameOverMenu.SetActive(false);

		isGameOver = false;

		


	}
}




