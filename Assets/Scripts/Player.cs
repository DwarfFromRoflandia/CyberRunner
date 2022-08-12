using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour,IBeginDragHandler,IDragHandler
{
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject secondStartPoint;
	private Transform player�oordinates;

	 
	public float MaxDistance, MinDistance;
	public float PlayerSpeed;

	[SerializeField] private ShotGun shot;
	public Transform StartPoint;
	Animator Player_Anim;
	public float TimeBeReady = 3;
	[SerializeField] private SpawnManager spawnManager;
	public float MinLenghtOfTouch = 8; // ����������� ����� ������ ��� ����������� ������

	public Text TextTimeToStart;
	[SerializeField] private Slider HealthSlider;
	private ButtonController ButtonPress;

	[SerializeField] private Sprite Play, Stop;
	[SerializeField] private ParticleSystem ParticleInCoin;
	public Image PauseImage;
	private void Start()
	{
		Player_Anim = GetComponent<Animator>();
	    ButtonPress = GameObject.Find("MainMenuCanvas").GetComponent<ButtonController>();
		player�oordinates = GetComponent<Transform>();

		//EventManager.GameOverEvent.AddListener(GameOver);
	}

    private void Update()
    {
		GameOver();
	}

    public void False()
	{
		Player_Anim.SetBool("PlayIsPressed", false);
		Player_Anim.SetFloat("Speed", 40);

		gameObject.AddComponent<Rigidbody>();

		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;

	}

	public void GameOver()//�����, ���������� �� ����� ����
	{
		if (HealthSlider.value <= 0.1)
		{
			Debug.Log(player�oordinates.transform.position);
			Player_Anim.SetFloat("Speed", 0);
			EventManager.EventPlay?.Invoke(0);
			gameOverMenu.SetActive(true);
		}

	}

	public void ButtonExitToMainMenu()
	{
		mainMenu.SetActive(true);
		gameOverMenu.SetActive(false);
		player�oordinates.transform.position = secondStartPoint.transform.position;
		HealthSlider.value = 1;
		SceneManager.LoadScene(0);
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

			 
		


		}
    }

	


    private void OnEnable()
	{
		EventManager.EventPlay += StartRunValues;
		EventManager.Animation_Play += Set_Anim_Play_True;
		 

	}
	private void OnDisable()
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
		
	
		transform.Translate(0, 0, GameSpeed * Time.fixedDeltaTime, Space.World);

	 
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


	public void Clic()
	{

		MovePerson(Input.mousePosition);


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


	public void MovePerson(Vector2 Pos) // ��� ��
	{

		if (!ButtonPress.Main_Menu_Condition.activeSelf && PlayerSpeed > 0)
		{



			if (Pos.x > Camera.main.pixelWidth / 2)
			{
				Player_Anim.SetBool("MoveRight", true);

				StartCoroutine(StartRollingRight());

			}

			if (Pos.x < Camera.main.pixelWidth / 2)
			{

				Player_Anim.SetBool("MoveLeft", true);


				StartCoroutine(StartRollingLeft());






			}

		}



	}
	Vector3 Moving = Vector3.zero;
	public IEnumerator StartRollingRight()
	{
	 

		float TimeStart = Time.time;

		float TimeToGo = Time.time + 0.8f;

		while (TimeStart < TimeToGo) 
		{
			TimeStart = Time.time;
		
			Moving = Vector3.MoveTowards(transform.position,
					   new Vector3(transform.position.x + 66.2f, transform.position.y, transform.position.z),
					   100 * Time.fixedDeltaTime);

			Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

			transform.position = Moving;
			yield return null;
		}
	
	}

	public IEnumerator StartRollingLeft()
	{
		 

			float TimeStart = Time.time;

			float TimeToGo = Time.time + 0.8f;
		
			while (TimeStart < TimeToGo)  
			{
				TimeStart = Time.time;
			
			Moving = Vector3.MoveTowards(transform.position,
						   new Vector3(transform.position.x - 66.2f, transform.position.y, transform.position.z),
						   100 * Time.fixedDeltaTime);

			Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

			transform.position = Moving;

			yield return null;

			}


	}

	 
	public void StartShot()
	{
		shot.Shot();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		print("����������");
		if ((eventData.delta.y) > 0)
		{

			print("������ �����!");

		}
		else
		{

			print("������� ����");
		
		
		}
	}
	

	public void OnDrag(PointerEventData eventData)
	{
		throw new NotImplementedException();
	}
}




