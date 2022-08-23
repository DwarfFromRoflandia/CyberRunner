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

	private void Awake()
	{
		Source_Crash = GetComponent<AudioSource>();

		SerializeDict(Dict);
	 
	}

	private void OnEnable()
	{
		EventManager.AudioStartRun += StartRunClip;
		EventManager.AudioMove += AudioPlayJumpOrScroll;
	}
	private void OnDisable()
	{
		EventManager.AudioStartRun -= StartRunClip;
		EventManager.AudioMove -= AudioPlayJumpOrScroll;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Obstacle")
		{
			Source_Crash.loop = false;
			Source_Crash.PlayOneShot(Dict["Box"]);


		}

		else if (collision.gameObject.tag == "MetalObstacle")
		{

			Source_Crash.loop = false;
			Source_Crash.PlayOneShot(Dict["Metal"]);



		}

		else if (collision.gameObject.tag == "Car")
		{


			Source_Crash.loop = false;
			Source_Crash.PlayOneShot(Dict["Car"]);

		}



		Source_Crash.clip = Dict["Run"];
		Source_Crash.loop = true;
		Source_Crash.Play();
		
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
	private void StartRunClip()//воспроизводит клип бега
	{
		Source_Crash.volume = 0.5f;
		Source_Crash.clip = Dict["Run"];
		Source_Crash.loop = true;
		Source_Crash.Play();
		
	}

	private void AudioPlayJumpOrScroll()
	{

		 
		Source_Crash.PlayOneShot(Dict["Jump"]);//оспроизводит клип прыжка и отскоков
	 
		
		 
	}
	
	
}
