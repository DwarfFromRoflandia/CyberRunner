using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonController :MonoBehaviour
{
   public float GameSpeed;
    public GameObject Main_Menu_Condition; // ������ �� ������� ����
    public Animation Menu_Settings; // ������ �� ���� ��������


    public void InputPlay() /* ����� ��� ������������� �������� ����������� ������� ������ 
                              (��� ������� �� ������ Play � ������� ����)*/
    {
        Main_Menu_Condition.SetActive(false);
        EventManager.EventPlay?.Invoke(GameSpeed);
    }

    public void InputSettings()
	{
        Main_Menu_Condition.SetActive(false); 
        Menu_Settings.Play();

    }

   public void ExitSettings()
    {
        RectTransform size = GameObject.Find("ButtonExit").GetComponent<RectTransform>();
        if (Input.GetMouseButtonDown(0))
        {

           //���� ������ ������ - �� ��� �� ������
        
        }

        if (Input.GetMouseButtonUp(0))
        { 
        // ���� ������� ������ �� ���-�� ������
        }
    
    }
     

    
}
