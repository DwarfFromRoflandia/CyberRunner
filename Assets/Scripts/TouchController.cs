using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchController : MonoBehaviour, IDragHandler,IEndDragHandler
{
	public float MaxDistance, MinDistance;
	//[SerializeField] private ButtonController ButtonPress;
	[SerializeField] private GameObject Player;

	
	[SerializeField] private Animator anim;
	public float PlayerSpeed;
	private Player player;

	private bool ObjectInside = false;

	void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Player>();

		if (SceneManager.GetActiveScene().buildIndex.Equals(1) == false) gameObject.SetActive(false); // ���� �� ��������� � ����� PplayMode -����� ����� ������������
	}

	
	private void OnTriggerStay(Collider colider)
	{

		ObjectInside= true;

	}

	private void OnTriggerExit(Collider colider)
	{

		ObjectInside = false;

	}
	public void OnDrag(PointerEventData eventData)
	{
		if (SceneManager.GetActiveScene().buildIndex.Equals(1)) // ���� �� ��������� � ����� PplayMode -����� ����� ������������
		{
			bool XGreatherY = Mathf.Abs(eventData.delta.y) < Mathf.Abs(eventData.delta.x); //���������� ����������� ����� ����� ������ �� �����

			if (eventData.delta.y > 0  && XGreatherY == false && !ObjectInside) // ���� ��������� �����
			{

				anim.SetBool("Scroll", true);

 




			}

			else if (eventData.delta.x > 0 && XGreatherY == true)
			//������ ����  ����� ����������� ������ �� x ������ ����� ����������� �� y - ���� �������������� . ��������� �� �����
			{
				anim.SetBool("MoveRight", true);

				StartCoroutine(StartRollingRight());
			}
			else if (eventData.delta.x < 0 && XGreatherY == true)
			{


				anim.SetBool("MoveLeft", true);


				StartCoroutine(StartRollingLeft());
			}





			else if (eventData.delta.y < 0 && XGreatherY == false && !ObjectInside)//������� 
			{
				 

				anim.SetBool("RollForw", true);


				player.rb.velocity = player.transform.forward * Time.fixedDeltaTime * 15_00;


			}
			 

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
		anim.SetBool("MoveLeft", false);
		anim.SetBool("MoveRight", false);


		EventManager.AudioMove.Invoke();// ��������� ���� �����������

		anim.SetBool("RollForw", false);
	}


	 
	

}
