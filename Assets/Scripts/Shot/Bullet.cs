using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 [SerializeField] private float ForceBullet = 2;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0,0,ForceBullet*Time.fixedDeltaTime);
    }
	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Obstacle")) //препятствия которые можно поразить - под тэгом Obstacle
		{


			Destroy(gameObject,0.5f);
		
		}
	}
}
