using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class TouchController : MonoBehaviour, IDragHandler,IEndDragHandler
{
	public int MaxDistance, MinDistance;
 
	[SerializeField] private GameObject Player;

	
	[SerializeField] private Animator anim;
	public float PlayerSpeed;
 

	
	void Awake()
	{

	
 

		if (SceneManager.GetActiveScene().buildIndex.Equals(1) == false) gameObject.SetActive(false); // если мы находимс€ в сцене PplayMode -тогда можем перемещатьс€
	}
	 
	public void OnDrag(PointerEventData eventData)
	{
		
			bool XGreatherY = Mathf.Abs(eventData.delta.y) < Mathf.Abs(eventData.delta.x); //переменна€ провер€юща€ какой свайп больше по длине




		if (eventData.delta.y > 0 && XGreatherY == false) // если свайпнули вверх
		{

			anim.SetBool("Scroll", true);

		}

		else if (eventData.delta.x > 0 && XGreatherY == true  )
		//тоесть если  длина перемещени€ пальца по x больше длины перемещени€ по y - тода поворачиваемс€ . »збавл€ет от багов
		{
			anim.SetBool("MoveRight", true);

			StartCoroutine(StartRolling(MaxDistance));
		}
		else if (eventData.delta.x < 0 && XGreatherY == true )
		{


			anim.SetBool("MoveLeft", true);


			StartCoroutine(StartRolling(MinDistance));
		}





		else if (eventData.delta.y < 0 && XGreatherY == false)//перекат 
		{


			anim.SetBool("RollForw", true);





		}


		 
		
		
	}





	void ResetAllSettings()
	{
		anim.SetBool("Scroll", false);
		anim.SetBool("MoveLeft", false);
		anim.SetBool("MoveRight", false);
		anim.SetBool("RollForw", false);
	}




	
 

	public IEnumerator StartRolling (int Distance)
	{
		Vector3 Coord = Vector3.zero;

	 


		while ((int)Coord.x!=Distance )
		{
			Coord = Player.transform.position;

			Player.transform.position = Vector3.MoveTowards(Player.transform.position,
				new Vector3(Distance,
				Player.transform.position.y,
				Player.transform.position.z),
				100f * Time.deltaTime);

			yield return null;

		}


		StopAllCoroutines();

	}

	 

	public void OnEndDrag(PointerEventData eventData)
	{
		ResetAllSettings();

		EventManager.AudioMove.Invoke();// запускаем звук перемещени€

		 
	}


	 
	

}
