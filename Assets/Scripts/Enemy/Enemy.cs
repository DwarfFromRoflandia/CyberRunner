using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class Enemy : MonoBehaviour
{
	[SerializeField] public float ObstacleDamage = 0.2f;
	private void Start()
	{
		 
	}
	public float GetDamage(float s )
	{
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
	public IEnumerator Object_Disapear( GameObject Body)
	{

		Renderer rend = Body.GetComponent<Renderer>();//получаем компонент рендеринга обьекта

		Color color = rend.material.color;


		while (color.a>0)
		{



			color.a -= 2f * Time.deltaTime;


			rend.material.color = color;
			yield return null;
		}



	}
}
