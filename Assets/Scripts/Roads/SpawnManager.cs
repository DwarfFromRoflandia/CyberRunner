using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
 private RoadSpawner roadSpawner;
    private Light[] Lights;

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "LoadScene")
        {

            roadSpawner = GameObject.Find("Roads").GetComponent<RoadSpawner>();
            roadSpawner.MoveRoad();

            /*����� ������� ���� ������������� ��� ���������� �����.
 * ��� �� ���������������� �� ���� ���� �� � ���������� �����.
 * ��� ������������� ������� ������ ���������*/
            Lights = FindObjectsOfType<Light>();
          
            Destroy(Lights[1]);
           
         

            Destroy(gameObject);
        }

    }
        

    }
	  
       




