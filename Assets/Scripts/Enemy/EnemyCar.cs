using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCar : Enemy
{

    [SerializeField] private float speed = 0;
    [SerializeField] private float SignalStartDistance = 400;
    private Rigidbody rb;
    [SerializeField] private Material CarMaterial;
    [SerializeField] private Texture CarText;
	private void OnCollisionEnter(Collision collision)
	{
        if(collision.transform.CompareTag("Player"))
        GetComponent<AudioSource>().Play();

        if (collision.transform.CompareTag("Bullet")|| collision.transform.CompareTag("Player"))
        {
            CarMaterial.mainTexture = CarText;
            gameObject.GetComponent<Renderer>().material = CarMaterial;// ������� ������� �������� ������������
            StartCoroutine(Object_Disapear(gameObject));
        }

        if (collision.transform.CompareTag("Destroyer"))//���������� ������ ����� ����� �������� ��
		{
            Destroy(gameObject);
		}
        
	}
	private void OnEnable()
	{
        EventManager.SetSpeedCar.AddListener(Speed_Car);
	}

	



	// Start is called before the first frame update
	void Start()
    {
        ObstacleDamage = 0.4f;
        rb = GetComponent<Rigidbody>();

    }
    


    // Update is called once per frame
    void FixedUpdate()
    {


        rb.velocity = transform.forward*speed * Time.fixedDeltaTime;
        
    }

    void Speed_Car(float speed)
    {
        this.speed = speed;
    }
}
