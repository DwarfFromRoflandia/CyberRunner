using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
	[SerializeField] private const float ObstacleDamage = 0.2f;

	public  float Speed = 0;
	
	public float GetDamage()
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

	public IEnumerator Object_DisapearSecondRoad(GameObject Body)//делаем угасание при соприкосновении
	{ 
		 
			Renderer rend = Body.GetComponent<Renderer>();//получаем компонент рендеринга обьекта

		 
			Color color = rend.material.color;


			while (color.a > 0 && color != null)
			{
			if (rend != null)
			{



				color.a -= 2f * Time.deltaTime;


				rend.material.color = color;



				 
			}
			yield return null;

		}
		
		 

	 
	}


	 
}
