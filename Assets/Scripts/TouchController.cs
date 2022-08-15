using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IDragHandler,IEndDragHandler
{
	public float MaxDistance, MinDistance;
	//[SerializeField] private ButtonController ButtonPress;
	[SerializeField] private GameObject Player;
	private Rigidbody PlayerRb;
	[SerializeField] private float JumpHeight=60; //высота прыжка
	private RaycastHit hit;
	[SerializeField] private Animator anim;
	public float PlayerSpeed;
	void Awake()
	{
		 
	}

	void FixedUpdate()
	{
		//блок гравитации
		Physics.Raycast(Player.transform.position, Vector3.down, out hit); //создаем луч дл€ проверки

		if ((hit.distance > JumpHeight&&(hit.transform.tag.Equals("Road") || hit.transform.tag.Equals("Obstacle"))) ) // если мы прыгнули выше высоты
																													  // прыжка и под нами дорога или преп€тствие - тогда падаем
		{
			
			print("јпдейт");
			PlayerRb.velocity = Vector3.zero;
			PlayerRb.velocity += Vector3.down * Time.deltaTime * 15_000;
			 
		 
		
		}
	
	
	}
	public void OnDrag(PointerEventData eventData)
	{
		bool XGreatherY = Mathf.Abs(eventData.delta.y) < Mathf.Abs(eventData.delta.x); //переменна€ провер€юща€ какой свайп больше по длине
		PlayerRb = Player?.GetComponent<Rigidbody>();
		if (eventData.delta.y > 0 && hit.distance < 1 && XGreatherY == false) // если свайпнули вверх
		{

			anim.SetBool("Scroll", true);









		}

		else if (eventData.delta.x > 0 && XGreatherY == true)
		//тоесть если  длина перемещени€ пальца по x больше длины перемещени€ по y - тода поворачиваемс€ . »збавл€ет от багов
		{
			anim.SetBool("MoveRight", true);

			StartCoroutine(StartRollingRight());
		}
		else if (eventData.delta.x < 0 && XGreatherY == true)
		{


			anim.SetBool("MoveLeft", true);


			StartCoroutine(StartRollingLeft());
		}





		else if (eventData.delta.y < 0 && XGreatherY == false)
		{

			anim.SetBool("RollForw", true);
		
		
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

			Moving = Vector3.MoveTowards(Player.transform.position,
					   new Vector3(Player.transform.position.x + 20f, Player.transform.position.y, transform.position.z),
					   100 * Time.deltaTime);

			Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

			Player.transform.position = Moving;
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

			Moving = Vector3.MoveTowards(Player.transform.position,
						   new Vector3(Player.transform.position.x - 20f, Player.transform.position.y, transform.position.z),
						   100 * Time.deltaTime);

			Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance);

			Player.transform.position = Moving;

			yield return null;

		}


	}

	 

	public void OnEndDrag(PointerEventData eventData)
	{
		anim.SetBool("Scroll", false);

		anim.SetBool("RollForw", false);
	}
}
