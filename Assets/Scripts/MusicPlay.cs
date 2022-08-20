using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    [SerializeField] private AudioSource ButtonSource;
    [SerializeField] private AudioClip ButtonClip;
    void ButtonClic()
    {
       
        ButtonSource.volume = 0.8f;
       
        ButtonSource.PlayOneShot(ButtonClip);
    
    }
	private void OnEnable()
	{
        EventManager.ButtonClicked += ButtonClic;

	}
    private void OnDisable()
    {
        EventManager.ButtonClicked -= ButtonClic;

    }
}
