using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCar: MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;
    private AudioSource au;
	private void Start()
	{
       au =  gameObject.GetComponentInParent<AudioSource>();
       
       
    }
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player"))
        au.Play();

    }
   
}
