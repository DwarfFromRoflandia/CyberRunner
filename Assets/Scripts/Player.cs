using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

	private float BufferSpeed;

	private void OnEnable()
	{
		EventManager.EventPlay += StartRunValues;
	}
	private void OnDisable()
	{
		EventManager.EventPlay -= StartRunValues;

	}



	private void StartRunValues(float GameSpeed)
	{
		BufferSpeed = GameSpeed;

		transform.Translate(0,0,GameSpeed*Time.fixedDeltaTime);
		

	}
	private void FixedUpdate()
	{
		StartRunValues(BufferSpeed);
	}


}
