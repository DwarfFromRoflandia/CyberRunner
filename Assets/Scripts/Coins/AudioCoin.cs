using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCoin : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        EventManager.AudioCoinEvent.AddListener(Sound);
    }

    private void Sound()
    {
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
    
    }
}
