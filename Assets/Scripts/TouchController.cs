using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	[SerializeField] private GameObject Player;
	private Rigidbody PlayerRb;
	[SerializeField] private float JumpHeight=60; //высота прыжка
	private RaycastHit hit;
	[SerializeField] private Animator anim;
	void Awake()
	{
		 
	}
	public void OnBeginDrag(PointerEventData eventData) // срабатывает когда палец начинает перемещаться
	{
		if (eventData.delta.y > 0 && hit.distance < 1)
		{


			anim.SetTrigger("Jump");

			PlayerRb = Player?.GetComponent<Rigidbody>();


			PlayerRb.velocity = Vector3.up * Time.deltaTime * 8000;// придаем силу вверх персонажу когда свайпнули вверх



		}

		else if (eventData.delta.y < 0 && hit.distance < 1)
		{

			anim.SetTrigger("Scroll");


		}

		 
	}
	void FixedUpdate()
	{
		Physics.Raycast(Player.transform.position, Vector3.down, out hit); //создаем луч для проверки

		if ((hit.distance > JumpHeight&&(hit.transform.tag.Equals("Road") || hit.transform.tag.Equals("Obstacle"))) ) // если мы прыгнули выше высоты
																													  // прыжка и под нами дорога или препятствие - тогда падаем
		{
			print("Апдейт");
			PlayerRb.velocity = Vector3.zero;
			PlayerRb.velocity += Vector3.down * Time.deltaTime * 4000;
			PlayerRb.velocity += Vector3.forward * Time.deltaTime * 2000;
		 
		
		}
	
	
	}
	public void OnDrag(PointerEventData eventData)
	{
		print("");
	}
}
