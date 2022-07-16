using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

	private float BufferSpeed;
	Animator Player_Anim;
	 

	public float MinLenghtOfTouch = 8; // ����������� ����� ������ ��� ����������� ������

	private void Start()
	{
		Player_Anim = GetComponent<Animator>();
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
		BufferSpeed = GameSpeed;

		transform.Translate(0, 0, GameSpeed * Time.fixedDeltaTime, Space.World);


	}

 
	 
		private void FixedUpdate()
	{
		StartRunValues(BufferSpeed);

		if (Input.touchCount > 0 && !Physics.Linecast(transform.position, Camera.main.transform.position))
		{


			Touch touch = Input.GetTouch(0);


			MovePerson( touch); // �����  �������� �������� ���� ����� ������������


		
		}//��� ���������


		if (Input.GetMouseButton(0)&&!Physics.Linecast(transform.position,Camera.main.transform.position)) // ��� ��
		{

			MovePerson(Input.mousePosition);
			Player_Anim.SetBool("TurnRight", true);
			Player_Anim.SetBool("TurnLeft", true);



		}


		if (Input.GetMouseButtonUp(0))
		{


			Player_Anim.SetBool("",false);
			Player_Anim.SetBool("",false);



		}





	}

	private void Set_Anim_Play_True(bool b)
	{

		Player_Anim.SetBool("PlayIsPressed", b);

	}

	private void MovePerson(Touch touch)

	{
	


			if (touch.phase == TouchPhase.Moved && touch.deltaPosition.x > MinLenghtOfTouch)
			{

				transform.position += new Vector3(20, 0, 0);
			 

			}


			else if (touch.phase == TouchPhase.Moved && touch.deltaPosition.x < -MinLenghtOfTouch)
			{

				transform.position -= new Vector3(20, 0, 0);
			

			}

			else if (touch.phase == TouchPhase.Ended)
			{
			
			
		}
	}


	private  void  MovePerson(Vector2 Pos) // ��� ��
	{
	 
		
		
			if (Pos.x > Camera.main.pixelWidth / 2)
			{
		 
			Player_Anim.SetBool("TurnRight", true);
			transform.position = Vector3.Lerp(transform.position,
					new Vector3(transform.position.x + 20, transform.position.y, transform.position.z),
					20 * Time.fixedDeltaTime);

			}

			if (Pos.x < Camera.main.pixelWidth / 2)
			{
		 
			transform.position = Vector3.Lerp(transform.position,
					new Vector3(transform.position.x - 20, transform.position.y, transform.position.z),
					20 * Time.fixedDeltaTime);


			}

			 



	}


	public void Include_to_Run()
	{
		Player_Anim.SetBool("TurnLeft", false);
		Player_Anim.SetBool("TurnRight",false);
	}












}




