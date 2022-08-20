using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IDragHandler,IEndDragHandler
{
	public float MaxDistance, MinDistance;
	//[SerializeField] private ButtonController ButtonPress;
	[SerializeField] private GameObject Player;

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
	 

		//���� ����������
		 
		 

	}
	public void OnDrag(PointerEventData eventData)
	{
		bool XGreatherY = Mathf.Abs(eventData.delta.y) < Mathf.Abs(eventData.delta.x); //���������� ����������� ����� ����� ������ �� �����
		 
		if (eventData.delta.y > 0 && hit.distance < 3 && XGreatherY == false) // ���� ��������� �����
		{

			anim.SetBool("Scroll", true);

		

			



		}

		 if (eventData.delta.x > 0 && XGreatherY == true)
		//������ ����  ����� ����������� ������ �� x ������ ����� ����������� �� y - ���� �������������� . ��������� �� �����
		{
			anim.SetBool("MoveRight", true);

			StartCoroutine(StartRollingRight());
		}
		 if (eventData.delta.x < 0 && XGreatherY == true)
		{


			anim.SetBool("MoveLeft", true);


			StartCoroutine(StartRollingLeft());
		}





		else if (eventData.delta.y < 0 && XGreatherY == false)//������� 
		{

			anim.SetBool("RollForw", true);
			

			player.rb.velocity = player.transform.forward * Time.fixedDeltaTime * 15_00;


		}

		 


	}




 





	
	public IEnumerator StartRollingRight()
	{
		

		float TimeStart = Time.time;

		float TimeToGo = Time.time + 0.8f;

		while (TimeStart < TimeToGo)
		{

			TimeStart = Time.time;

			Player.transform.position = Vector3.MoveTowards(Player.transform.position,
				new Vector3(MaxDistance,
				Player.transform.position.y,
				Player.transform.position.z),
				100f*Time.deltaTime);


	

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

			Player.transform.position = Vector3.MoveTowards(Player.transform.position,
				new Vector3(MinDistance,
				Player.transform.position.y,
				Player.transform.position.z),
				100f * Time.deltaTime);

			yield return null;

		}


	}

	 

	public void OnEndDrag(PointerEventData eventData)
	{
		anim.SetBool("Scroll", false);
	

		anim.SetBool("RollForw", false);
	}


	 
	

}
