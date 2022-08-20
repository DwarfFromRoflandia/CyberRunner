using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitGame : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;  
    public void UWantExitGame()
    {
        EventManager.ButtonClicked.Invoke();// �������� ���� ������� 

        exitPanel.SetActive(true);
    }
    public void ExitTheGame()
    {
        EventManager.ButtonClicked.Invoke();// �������� ���� ������� 
        Application.Quit();
        Debug.Log("Exit the Game");
    }

    public void DontExitTheGame()
    {
        EventManager.ButtonClicked.Invoke();// �������� ���� ������� 
        exitPanel.SetActive(false);
    }
}
