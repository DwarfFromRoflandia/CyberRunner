using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager  : MonoBehaviour
{
	[Header("List")]

	private AudioSource Source_Crash;
	[SerializeField] private List <AudioClip> Clips;
	[SerializeField] private List<string> Values;
	private Dictionary<string, AudioClip> Dict = new Dictionary<string, AudioClip>();
	bool StayIn;
	private void Awake()
	{
		Source_Crash = GetComponent<AudioSource>();

		SerializeDict(Dict);
	 
	}
	
	private void OnEnable()
	{
		
		EventManager.AudioMove += AudioPlayJumpOrScroll;
	
	}
	private void OnDisable()
	{
		EventManager.AudioMove -= AudioPlayJumpOrScroll;
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		StayIn = true;
		 
		if (collision.gameObject.tag == "Obstacle" )
		{
			
			Source_Crash.loop = false;
			Source_Crash.PlayOneShot(Dict["Box"]);


		}

		else if (collision.gameObject.tag == "MetalObstacle" )
		{

			Source_Crash.loop = false;
			Source_Crash.PlayOneShot(Dict["Metal"]);



		}

		else if (collision.gameObject.tag == "Car")
		{


			Source_Crash.loop = false;
			Source_Crash.PlayOneShot(Dict["Car"]);

		}


;
		
	}
	
	private void SerializeDict(Dictionary<string,AudioClip> dict) // заполняет переданный словарь 
	{
		int i = 0;
		foreach (string name in Values)
		{

			dict.Add(name, Clips[i]);
			i++;



		}

	}
	


	private void AudioPlayJumpOrScroll()
	{

		Source_Crash.Stop();
		Source_Crash.PlayOneShot(Dict["Jump"]);//оспроизводит клип прыжка и отскоков

		 
	}


}
