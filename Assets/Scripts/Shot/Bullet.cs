using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 [SerializeField] private float ForceBullet = 2;
	[SerializeField] private float Force; // �������� ����� ������������
	[SerializeField] private ParticleSystem ParticleBullet; // ������������
	private Player Player;
	private void Awake()
	{
		Player = GameObject.Find("Player").GetComponent<Player>();
		 
	}
	void FixedUpdate()
    {
		
        transform.Translate(0, (((Player.PlayerSpeed)+ForceBullet)*Time.fixedDeltaTime), 0,Space.Self);
    }
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Enemy>()!=null) //����������� ������� ����� �������� 
		{
			ParticleSystem ParticleUnit = Instantiate(ParticleBullet,transform.position,Quaternion.identity); //������� ������ ������������ � ����� ��� �������� �������
			ParticleUnit.Play();//��������� ������ ������������

			Destroy(ParticleUnit,2f); //������� ����������� ������ � ������ ���� 

			Destroy(gameObject,0.1f);
			Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>(); /* ������� ���� ������ �����
			                                                                * ��� ������������ */
			rb.AddForce(Vector3.forward*Time.deltaTime*Force); //Time.deltaTime ����� ��� ���� ����� ���� ���� ���������� �� ���� �����������

			Enemy enemy = collision.gameObject.GetComponent<Enemy>();//�������� ����� ���� ��� ������������ ����
			if (collision.transform.tag != "Car")
			{
				StartCoroutine(enemy.Object_Disapear(collision.gameObject)); // ��������� �������� ������������
			}
			Destroy(collision.gameObject,0.5f);//������� ����� 2 ������ �����
			 

		}
		Player = GameObject.Find("Player").GetComponent<Player>();
	}
}
