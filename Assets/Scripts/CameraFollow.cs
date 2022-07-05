using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;

 
    public float DistanceFromPlayer;


 
    void LateUpdate()
    {

        Vector3 EndPoint = new Vector3(transform.position.x, transform.position.y,  Player.transform.position.z-DistanceFromPlayer);

        transform.position = Vector3.Lerp(transform.position, EndPoint,0002f*Time.deltaTime);

        transform.LookAt(Player.transform);




   }
}
