using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float MaxDistance, MinDistance;
	public float PlayerSpeed;
	public Transform StartPoint;
	Animator Player_Anim;
	public float TimeBeReady = 3;
	[SerializeField] private SpawnManager spawnManager;
	public float MinLenghtOfTouch = 8; // минимальная длина свайпа для перемещения игрока

	public Text TextTimeToStart;

	[SerializeField] private Sprite Play, Stop;
	public Image PauseImage;
	private void Start()
	{
		Player_Anim = GetComponent<Animator>();
		 
	}
	public void False()
	{
		Player_Anim.SetBool("PlayIsPressed", false);
		Player_Anim.SetFloat("Speed", 40);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "SpawnTrigger")
		{
			spawnManager.SpawnTriggerEntered();
		}

		if (other.tag == "Coin")
		{
			Destroy(other.gameObject);
			if (EventManager.PickUpCoinEvent != null) EventManager.PickUpCoinEvent.Invoke();
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
		print("Было");
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

	private void FixedUpdate()
	{
		StartRunValues(PlayerSpeed);

		if (Input.touchCount > 0 && !Physics.Linecast(StartPoint.position, Camera.main.transform.position))
		{


			Touch touch = Input.GetTouch(0);

			transform.localRotation = Quaternion.Euler(transform.rotation.x, 360, transform.rotation.z);


			MovePerson(touch); // метод  задающий движение если палец переместился



		}//для телефонов


		if (Input.GetMouseButton(0) && !Physics.Linecast(StartPoint.position, Camera.main.transform.position)) // для пк
		{

			MovePerson(Input.mousePosition);


			transform.localRotation = Quaternion.Euler(transform.rotation.x, 364, transform.rotation.z);



		}


		if (Input.GetMouseButtonUp(0))
		{





		}





	}

	private void Set_Anim_Play_True(bool b)
	{

		Player_Anim.SetBool("PlayIsPressed", b);

	}

	private void MovePerson(Touch touch)

	{

		Vector3 Moving = Vector3.zero;

		if (touch.phase == TouchPhase.Moved && touch.deltaPosition.x > MinLenghtOfTouch)
		{

			Player_Anim.SetBool("MoveRight", true);

			//Moving = Vector3.Lerp(transform.position,
			//		new Vector3(transform.position.x - 10, transform.position.y, transform.position.z),
			//		20 * Time.fixedDeltaTime);



		}


		else if (touch.phase == TouchPhase.Moved && touch.deltaPosition.x < -MinLenghtOfTouch)
		{
			Player_Anim.SetBool("MoveLeft", true);


			//Moving = Vector3.Lerp(transform.position,
			//		new Vector3(transform.position.x + 10, transform.position.y, transform.position.z),
			//		20 * Time.fixedDeltaTime);


		}

		else if (touch.phase == TouchPhase.Ended)
		{


		}
		Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

		transform.position = Moving;
	}


	private void MovePerson(Vector2 Pos) // для пк
	{


		Vector3 Moving = Vector3.zero;

		if (Pos.x > Camera.main.pixelWidth / 2)
		{
			Player_Anim.SetBool("MoveRight", true);

			Moving = Vector3.MoveTowards(transform.position,
				   new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z),
				   1000 * Time.fixedDeltaTime);

		}

		if (Pos.x < Camera.main.pixelWidth / 2)
		{

			Player_Anim.SetBool("MoveLeft", true);



			Moving = Vector3.MoveTowards(transform.position,
				   new Vector3(transform.position.x - 1000, transform.position.y, transform.position.z),
				   1000 * Time.fixedDeltaTime);



		}
		Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

		transform.position = Moving;




	}


 











}




