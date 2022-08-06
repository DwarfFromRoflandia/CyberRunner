using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] public float ObstacleDamage = 0.2f;
	 
	public float GetDamage(float s)
	{
		print("было");
		return ObstacleDamage;
	}

	private void OnEnable()
	{
		EventManager.IsPunched += GetDamage;

	}
	private void OnDisable()
	{
		EventManager.IsPunched -= GetDamage;
	}
}
