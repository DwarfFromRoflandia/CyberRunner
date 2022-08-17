using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;  
    public void UWantExitGame()
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 

        exitPanel.SetActive(true);
    }
    public void ExitTheGame()
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
        Application.Quit();
        Debug.Log("Exit the Game");
    }

    public void DontExitTheGame()
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажатия 
        exitPanel.SetActive(false);
    }
}
