using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 [SerializeField] private float ForceBullet = 2;
	[SerializeField] private float Force; // значение силлы отталкивания
	[SerializeField] private ParticleSystem ParticleBullet; // столкновение
														 
	void FixedUpdate()
    {
        transform.position += new Vector3(0,0,ForceBullet*Time.fixedDeltaTime);
    }
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Obstacle")) //препятствия которые можно поразить - под тэгом Obstacle
		{
			ParticleSystem ParticleUnit = Instantiate(ParticleBullet,transform.position,Quaternion.identity); //создаем эффект столкновения в месте где сработал триггер
			ParticleUnit.Play();//запускаем эффект столкновения

			Destroy(ParticleUnit,2f); //удаляем проигранный эффект и префаб пули 

			Destroy(gameObject,0.5f);
			Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>(); /* придаем силу нашему врагу
			                                                                * для отталкивания */
			rb.AddForce(Vector3.forward*Time.deltaTime*Force); //Time.deltaTime нужен для того чтобы сила была одинаковой на всех устройствах

			Enemy enemy = collision.gameObject.GetComponent<Enemy>();//получаем класс Враг при столкновении пули

			StartCoroutine(enemy.Object_Disapear(collision.gameObject)); // запускаем коротину исчезновения

			Destroy(collision.gameObject,1f);//удаляем через 2 секунд врага

		}
	}
}
