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
	[SerializeField] private float distanceGaravit=3; //высота прыжка
	private RaycastHit hit;
	[SerializeField] private Animator anim;
	public float PlayerSpeed;
	private Player player;
	void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	void FixedUpdate()
	{
		//блок гравитации
		Physics.Raycast(Player.transform.position, Vector3.down, out hit); //создаем луч дл€ проверки

		if (hit.distance > distanceGaravit && (hit.transform.tag.Equals("Road") || hit.transform.tag.Equals("Obstacle"))
			&&player.rb!=null) // если мы прыгнули выше высоты
																// прыжка и под нами дорога или преп€тствие - тогда падаем
		{

			
			player.rb.velocity = Vector3.zero;
			player.rb.velocity += Vector3.down * Time.deltaTime * 15_000;



		}
		else if(player.rb!=null&&hit.distance<distanceGaravit)
		{

			player.rb.velocity = new Vector3(player.rb.velocity.x,0, player.rb.velocity.z);

		}
		Moving = Player.transform.position;

		Moving.x = Mathf.Clamp(Moving.x, MinDistance, MaxDistance); //задаем границы перемещени€ при прыжке
																	//потомучто наша анимаци€ непоправимо  сдвигаетс€ при проигрывании

		Player.transform.position = Moving;

	}
	public void OnDrag(PointerEventData eventData)
	{
		bool XGreatherY = Mathf.Abs(eventData.delta.y) < Mathf.Abs(eventData.delta.x); //переменна€ провер€юща€ какой свайп больше по длине
		 
		if (eventData.delta.y > 0 && hit.distance < 3 && XGreatherY == false) // если свайпнули вверх
		{

			anim.SetBool("Scroll", true);
			player.rb.velocity = Vector3.forward * Time.deltaTime * 15_00;


			 



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

		float TimeToGo = Time.time + 5f;

		while (TimeStart < TimeToGo)
		{
			TimeStart = Time.time;

			Moving = Vector3.MoveTowards(Player.transform.position,
					   new Vector3(Player.transform.position.x + 100f, Player.transform.position.y, transform.position.z),
					   0.1f * Time.deltaTime);

		 
			yield return null;
		}

	}

	public IEnumerator StartRollingLeft()
	{


		float TimeStart = Time.time;

		float TimeToGo = Time.time + 5f;

		while (TimeStart < TimeToGo)
		{
			TimeStart = Time.time;

			Moving = Vector3.MoveTowards(Player.transform.position,
						   new Vector3(Player.transform.position.x - 100f, Player.transform.position.y, transform.position.z),
						   0.1f* Time.deltaTime);

			 

			yield return null;

		}


	}

	 

	public void OnEndDrag(PointerEventData eventData)
	{
		anim.SetBool("Scroll", false);

		anim.SetBool("RollForw", false);
	}
}
