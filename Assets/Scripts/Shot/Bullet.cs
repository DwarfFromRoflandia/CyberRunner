using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 [SerializeField] private float ForceBullet = 2;
	[SerializeField] private float Force; // �������� ����� ������������
	[SerializeField] private ParticleSystem ParticleBullet; // ������������
														 
	void FixedUpdate()
    {
        transform.position += new Vector3(0,0,ForceBullet*Time.fixedDeltaTime);
    }
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Obstacle")) //����������� ������� ����� �������� - ��� ����� Obstacle
		{
			ParticleSystem ParticleUnit = Instantiate(ParticleBullet,transform.position,Quaternion.identity); //������� ������ ������������ � ����� ��� �������� �������
			ParticleUnit.Play();//��������� ������ ������������

			Destroy(ParticleUnit,2f); //������� ����������� ������ � ������ ���� 

			Destroy(gameObject,0.5f);
			Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>(); /* ������� ���� ������ �����
			                                                                * ��� ������������ */
			rb.AddForce(Vector3.forward*Time.deltaTime*Force); //Time.deltaTime ����� ��� ���� ����� ���� ���� ���������� �� ���� �����������

			Enemy enemy = collision.gameObject.GetComponent<Enemy>();//�������� ����� ���� ��� ������������ ����

			StartCoroutine(enemy.Object_Disapear(collision.gameObject)); // ��������� �������� ������������

			Destroy(collision.gameObject,1f);//������� ����� 2 ������ �����

		}
	}
}
