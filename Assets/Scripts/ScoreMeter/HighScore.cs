using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    [SerializeField] private MeterCounter _meterCounter; 
    private Text text;

    private int highScoreMeter;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SaveHighScoreMeter")) highScoreMeter = PlayerPrefs.GetInt("SaveHighScoreMeter");      
    }

    private void Update()
    {
        text.text = "Наивысший счёт: " + highScoreMeter + " m";
        ReassignHighScore();
    }


    private void ReassignHighScore()
    {
        if (_meterCounter.MeterCount > highScoreMeter)
        {
            highScoreMeter = _meterCounter.MeterCount;
            PlayerPrefs.SetInt("SaveHighScoreMeter", highScoreMeter);
        }    
    }
}
