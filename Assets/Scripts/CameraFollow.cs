using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


   public float DistanceFromPlayer;

    public GameObject Player;

 
   

  
 
    void LateUpdate()
    {

        Vector3 EndPoint = new Vector3(Player.transform.position.x, transform.position.y,  Player.transform.position.z-DistanceFromPlayer);

        transform.position = Vector3.Lerp(transform.position, EndPoint,2f*Time.deltaTime);

         




   }
}
