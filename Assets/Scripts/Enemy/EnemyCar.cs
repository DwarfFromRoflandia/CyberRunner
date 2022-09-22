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

        Object_DiapearRoad(collision,CarText,CarMaterial);

    }
	private void OnEnable()
	{
        EventManager.SetSpeedCar.AddListener(Speed_Car);
	}


    public void Object_DiapearRoad(Collision collision,Texture texture,Material material)
    {

        if (collision.transform.CompareTag("Bullet") || collision.transform.CompareTag("Player"))
        {
            material.mainTexture = CarText;

            gameObject.GetComponent<Renderer>().material = material;// придаем обьекту материал прозрачности

            StartCoroutine(Object_DisapearSecondRoad(gameObject));
        }

        if (collision.transform.CompareTag("Destroyer"))//уничтожаем машину когда игрок пробежал ее
        {
            Destroy(gameObject);
        }

    }


	// Start is called before the first frame update
 
  
    


    // Update is called once per frame
    void FixedUpdate()
    {

        print(Speed);
       transform.Translate(Vector3.forward * -Speed * Time.fixedDeltaTime,Space.World);
        
    }

    void Speed_Car(float speed)
    {
        Speed = speed;
    }
}
