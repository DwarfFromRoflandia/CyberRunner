using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
	 
	public float MaxDistance, MinDistance;
	public float PlayerSpeed;

	[SerializeField] private ShotGun shot;
	public Transform StartPoint;
	Animator Player_Anim;
	public float TimeBeReady = 3;
	[SerializeField] private SpawnManager spawnManager;
	public float MinLenghtOfTouch = 8; // минимальная длина свайпа для перемещения игрока

	public Text TextTimeToStart;
	[SerializeField] private Slider HealthSlider;
	private ButtonController ButtonPress;

	[SerializeField] private Sprite Play, Stop;
	[SerializeField] private ParticleSystem ParticleInCoin;
	public Image PauseImage;
	private void Start()
	{
		Player_Anim = GetComponent<Animator>();
	ButtonPress = 	GameObject.Find("MainMenuCanvas").GetComponent<ButtonController>();
		 


	}
	public void False()
	{
		Player_Anim.SetBool("PlayIsPressed", false);
		Player_Anim.SetFloat("Speed", 40);

		gameObject.AddComponent<Rigidbody>();

		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;

	}
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
        }

        if (other.transform.tag == "Coin")
        { 
            Instantiate(ParticleInCoin, other.transform.position + new Vector3(0, 8f, 0), other.transform.rotation);//при соприкосновении коллайдера игрока с монеткой появляется дымка от исчезнувшей монеты
			Destroy(other.gameObject);
            if (EventManager.PickUpCoinEvent != null) EventManager.PickUpCoinEvent.Invoke();
			if (EventManager.AudioCoinEvent != null) EventManager.AudioCoinEvent.Invoke();
        }

		if (other.transform.tag == "Obstacle")
		{

			HealthSlider.value -= EventManager.IsPunched.Invoke(0);// меняем значение здоровья игрока вызывая событие

			Enemy enemy = other.transform.GetComponent<Enemy>();

			StartCoroutine(enemy.Object_Disapear(enemy.gameObject));//передаем параметр предмета столкновения

			Destroy(other.gameObject, 1f);// удаляем врага через 3 секунды

			Player_Anim.SetTrigger("Punched"); // запускаем анимацию спотыкания

			
		


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
		while (TimeStart < TimeToGo) //3 секунды до старта игры
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
		 
	//		Vector3 Moving = Vector3.zero;

	//		if (touch.phase == TouchPhase.Moved && touch.deltaPosition.x > MinLenghtOfTouch)
	//		{

	//			Player_Anim.SetBool("MoveRight", true);

	//			//Moving = Vector3.Lerp(transform.position,
	//			//		new Vector3(transform.position.x - 10, transform.position.y, transform.position.z),
	//			//		20 * Time.fixedDeltaTime);



	//		}


	//		else if (touch.phase == TouchPhase.Moved && touch.deltaPosition.x < -MinLenghtOfTouch)
	//		{
	//			Player_Anim.SetBool("MoveLeft", true);


	//			//Moving = Vector3.Lerp(transform.position,
	//			//		new Vector3(transform.position.x + 10, transform.position.y, transform.position.z),
	//			//		20 * Time.fixedDeltaTime);


	//		}

	//		else if (touch.phase == TouchPhase.Ended)
	//		{


	//		}
	//		Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

	//		transform.position = Moving;
		 
	//}


	public void MovePerson(Vector2 Pos) // для пк
	{
		 
		if(!ButtonPress.Main_Menu_Condition.activeSelf)
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
	 









}




