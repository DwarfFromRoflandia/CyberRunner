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

	 
 
	public float PlayerSpeed;

	[SerializeField] private ShotGun shot;
	public Transform StartPoint;
	public Animator Player_Anim;
	public float TimeBeReady = 3;
	[SerializeField] private SpawnManager spawnManager;
	public float MinLenghtOfTouch = 8; // ����������� ����� ������ ��� ����������� ������

	public Text TextTimeToStart;
	[SerializeField] private Slider HealthSlider;
	 

	[SerializeField] private Sprite Play, Stop;
	[SerializeField] private ParticleSystem ParticleInCoin;
	private Rigidbody rb;
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

	}

	public void GameOver()//�����, ���������� �� ����� ����
	{
		 
		
			Debug.Log(player�oordinates.transform.position);
			Player_Anim.SetFloat("Speed", 0);
			EventManager.EventPlay?.Invoke(0);
			gameOverMenu.SetActive(true);
			EventManager.EventPlay += StartRunValues;


	}

	public void ButtonExitToMainMenu()
	{
		 
		SceneManager.LoadScene(0);
		mainMenu.SetActive(true);
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

			Enemy enemy = other.transform.GetComponent<Enemy>();

			StartCoroutine(enemy.Object_Disapear(enemy.gameObject));//�������� �������� �������� ������������

			Destroy(other.gameObject, 1f);// ������� ����� ����� 3 �������

			Player_Anim.SetTrigger("Punched"); // ��������� �������� ����������
			if (HealthSlider.value <= 0.1) GameOver(); // ��������� ������� ����� ����� ������ ��������� �� ������� ������






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
		StartRunValues(PlayerSpeed);
	}

	private void StartRunValues(float GameSpeed)
	{
		PlayerSpeed = GameSpeed;
 
		transform.Translate(0, 0, GameSpeed * Time.deltaTime, Space.World);

	 
	}

	 
	bool paused = true;
	public void Pause()
	{
 
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


	public void StartJump()
	{

		rb.velocity = Vector3.up * Time.deltaTime * 20_000;// ������� ���� ����� ��������� ����� ��������� �����
		rb.velocity += Vector3.forward * Time.deltaTime * 2000;


	}

	private void Set_Anim_Play_True(bool b)
	{

		Player_Anim.SetBool("PlayIsPressed", b);

	}

	//public void MovePerson(Touch touch)

	//{

	//	Vector3 Moving = Vector3.zero;



	//		Player_Anim.SetBool("MoveRight", true);

	//		Moving = Vector3.Lerp(transform.position,
	//				new Vector3(transform.position.x - 10, transform.position.y, transform.position.z),
	//				20 * Time.fixedDeltaTime);





	//		Player_Anim.SetBool("MoveLeft", true);


	//		Moving = Vector3.Lerp(transform.position,
	//				new Vector3(transform.position.x + 10, transform.position.y, transform.position.z),
	//				20 * Time.fixedDeltaTime);





	//	Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

	//	transform.position = Moving;

	//}


	 
	 
	public void StartShot()
	{
		shot.Shot();
	}

	

	public void OnDrag(PointerEventData eventData)
	{
		throw new NotImplementedException();
	}
}




