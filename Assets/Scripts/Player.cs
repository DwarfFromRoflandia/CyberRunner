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
	private Transform player�oordinates;
	private float speed;
	[SerializeField] private float distanceGravit = 3;

 

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
	private void Start()
	{	   
		player�oordinates = GetComponent<Transform>();
		EventManager.Animation_Play += Set_Anim_Play_True;
		EventManager.EventPlay += StartRunValues;
		EventManager.EventPlay?.Invoke(50);
		EventManager.Animation_Play?.Invoke(true);


		//EventManager.GameOverEvent.AddListener(GameOver);
	}


    public void False()
	{
		Player_Anim.SetBool("PlayIsPressed", false);
		Player_Anim.SetFloat("Speed", 40);

		gameObject.AddComponent<Rigidbody>();

		rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;

	}

	public void GameOver()//�����, ���������� �� ����� ����
	{
		Debug.Log(player�oordinates.transform.position);
		Player_Anim.SetFloat("Speed", 0);
		EventManager.EventPlay?.Invoke(0);
		gameOverMenu.SetActive(true);
		EventManager.EventPlay += StartRunValues;
		Debug.Log("GameOver");

	}
	
	public void ButtonExitToMainMenu()
	{
	 	 
		SceneManager.LoadScene(0);
		//mainMenu.SetActive(true);
		gameOverMenu.SetActive(false);
		player�oordinates.transform.position = secondStartPoint.transform.position;
		HealthSlider.value = 1;

		DontDestroyOnLoad(gameObject); /* ����� ��������� �� ������� �� ������� ������� � ����������� ��������� �� ����� ���������� ����� �����.
		                                * ������� �� �� ������� ������ � ��������� ��� �� ����� ����� ����� �� ��������� ������.������ � ����� ����� � ��� ����� 2 ��������� ������������.
		                              * ������� ������� ������� �� ���*/
		Destroy(gameObject, 0.1f);
	
	}

	private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
        }

        if (other.transform.tag == "Coin")
        { 
            Instantiate(ParticleInCoin, other.transform.position + new Vector3(0, 8f, 0), other.transform.rotation);//��� ��������������� ���������� ������ � �������� ���������� ����� �� ����������� ������
			Destroy(other.gameObject);
            if (EventManager.PickUpCoinEvent != null) EventManager.PickUpCoinEvent.Invoke();
			if (EventManager.AudioCoinEvent != null) EventManager.AudioCoinEvent.Invoke();
        }

		if (other.transform.tag == "Obstacle")
		{

			HealthSlider.value -= EventManager.IsPunched.Invoke(0);// ������ �������� �������� ������ ������� �������

			 

			 

			Destroy(other.gameObject, 1f);// ������� ����� ����� 3 �������

			Player_Anim.SetTrigger("Punched"); // ��������� �������� ����������
			if (HealthSlider.value <= 0.1) GameOver(); // ��������� ������� ����� ����� ������ ��������� �� ������� ������




			 

		}
		Enemy enemy = other.transform.GetComponent<Enemy>();

		StartCoroutine(enemy.Object_Disapear(other.gameObject));//�������� �������� �������� ������������
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
		StartRunValues(PlayerSpeed);

		Physics.Raycast(transform.position, Vector3.down, out hit); //������� ��� ��� ��������

		if (hit.distance > distanceGravit && (hit.transform.tag.Equals("Road") || hit.transform.tag.Equals("Obstacle"))
			&& rb != null) // ���� �� �������� ���� ������
								  // ������ � ��� ���� ������ ��� ����������� - ����� ������
		{


			rb.velocity = Vector3.zero;
			rb.velocity = -transform.up * Time.deltaTime * 15_000;



		}
		else if (rb != null && hit.distance < distanceGravit)
		{

			rb.velocity= new Vector3(rb.velocity.x, 0, speed);// �������� ���������

		}
	}

	private void StartRunValues(float GameSpeed)
	{
		PlayerSpeed = GameSpeed;
 
		transform.Translate(0, 0, GameSpeed * Time.deltaTime, Space.World);

	 
	}

	 
	bool paused = true;
	public void Pause()
	{

		EventManager.ButtonClicked.Invoke();
		if (paused)
		{
			EventManager.EventPlay?.Invoke(0);
			Player_Anim.SetFloat("Speed",0);
			paused = false;
			PauseImage.sprite = Stop;

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
		EventManager.EventPlay?.Invoke(50);
		Player_Anim.SetFloat("Speed", 50);


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




