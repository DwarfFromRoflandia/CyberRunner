using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
 private RoadSpawner roadSpawner;
    

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "LoadScene")
        {

            roadSpawner = GameObject.Find("Roads").GetComponent<RoadSpawner>();
            roadSpawner.MoveRoad();

            /*����� ������� ���� ������������� ��� ���������� �����.
 * ��� �� ���������������� �� ���� ���� �� � ���������� �����.
 * ��� ������������� ������� ������ ���������*/

           
         

          Destroy(gameObject);
        }

    }
        

    }
	  
       




