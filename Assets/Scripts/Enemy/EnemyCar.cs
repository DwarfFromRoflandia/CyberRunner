using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCar : Enemy
{
     
  
    [SerializeField] private Material CarMaterial;
    [SerializeField] private Texture CarText;
	private void OnCollisionEnter(Collision collision)
	{
        if(collision.transform.CompareTag("Player"))
        GetComponent<AudioSource>().Play();

        if (collision.transform.CompareTag("Bullet")|| collision.transform.CompareTag("Player"))
        {
            CarMaterial.mainTexture = CarText;

            gameObject.GetComponent<Renderer>().material = CarMaterial;// придаем обьекту материал прозрачности

            StartCoroutine(Object_Disapear(gameObject));
        }

        if (collision.transform.CompareTag("Destroyer"))//уничтожаем машину когда игрок пробежал ее
		{
            Destroy(gameObject);
		}
        
	}
	private void OnEnable()
	{
        EventManager.SetSpeedCar.AddListener(Speed_Car);
	}

	



	// Start is called before the first frame update
 
  
    


    // Update is called once per frame
    void FixedUpdate()
    {


       transform.Translate(Vector3.forward * -speed * Time.fixedDeltaTime,Space.World);
        
    }

    void Speed_Car(float speed)
    {
        this.speed = speed;
    }
}
