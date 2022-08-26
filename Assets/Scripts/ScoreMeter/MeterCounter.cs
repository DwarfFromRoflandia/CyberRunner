using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterCounter : MonoBehaviour
{
    private Text text;
    private float speed = 1;//��� ������ �������� ������ ����������, ��� � ������� ��������� ����� ���������� ����� � ������
    
    private int meterCount = 0;//���������� � ������� ����� ������������� �������� ����, ������� �������� �����
    public int MeterCount { get => meterCount;}

    private Player player;

    private void Start()
    {
        text = GetComponent<Text>();

        InvokeRepeating("Counter", 0, 1 / speed);//����� InvokeRepeating() �������� ����� Counter() ������� � 0 ������ ����� ������� ���� � ��������� ����� ������� ������ � ����������, ������� ����� � ���������� speed
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Counter()
    {
        if (player.IsGameOver == false && player.IsPauseOn == false)
        {
            meterCount += 1;
            text.text = meterCount.ToString();
        }
       
    }
}
