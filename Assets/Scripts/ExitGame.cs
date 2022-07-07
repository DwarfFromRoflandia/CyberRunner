using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;  
    public void UWantExitGame()
    {
        exitPanel.SetActive(true);
    }
    public void ExitTheGame()
    {
        Application.Quit();
        Debug.Log("Exit the Game");
    }

    public void DontExitTheGame()
    {
        exitPanel.SetActive(false);
    }
}
