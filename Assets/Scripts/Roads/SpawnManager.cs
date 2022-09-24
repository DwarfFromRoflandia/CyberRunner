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

            /*Юнити создает свет автоматически при добавлении сцены.
 * Оно не распространяется на билд игры но в инспекторе видно.
 * Для перестраховки удаляем лишнее освещение*/

           
         

          Destroy(gameObject);
        }

    }
        

    }
	  
       




