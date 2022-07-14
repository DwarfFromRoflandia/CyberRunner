using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

	private float BufferSpeed;
	Animator Player_Anim;

	private ButtonController butt_contr;
	public float MinLenghtOfTouch = 8; // ����������� ����� ������ ��� ����������� ������

	private void Start()
	{
		butt_contr = GameObject.Find("MainMenuCanvas").GetComponent<ButtonController>();
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

		if (Input.touchCount > 0&& !butt_contr.Main_Menu_Condition.activeSelf)
		{


			Touch touch = Input.GetTouch(0);


			MovePerson( touch); // �����  �������� �������� ���� ����� ������������


		
		}//��� ���������


		if (Input.GetMouseButton(0)&& !butt_contr.Main_Menu_Condition.activeSelf  ) // ��� ��
		{

			MovePerson(Input.mousePosition);


			 
		}


		if (Input.GetMouseButtonUp(0))
		{


			Player_Anim.SetBool("TurnRight", false);
			Player_Anim.SetBool("TurnLeft", false);

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
			Player_Anim.SetBool("TurnRight", true);

		}


		else if (touch.phase == TouchPhase.Moved && touch.deltaPosition.x < -MinLenghtOfTouch)
		{

			transform.position -= new Vector3(20, 0, 0);
			Player_Anim.SetBool("TurnLeft", true);

		}

		else if (touch.phase == TouchPhase.Ended)
		{
			Player_Anim.SetBool("TurnRight", false);
			Player_Anim.SetBool("TurnLeft",false);
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
			Player_Anim.SetBool("TurnLeft", true);
			transform.position = Vector3.Lerp(transform.position,
					new Vector3(transform.position.x - 20, transform.position.y, transform.position.z),
					20 * Time.fixedDeltaTime);


			}

 



	}



	
	 



	





}




