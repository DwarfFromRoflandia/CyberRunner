using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;


    public float DistanceFromPlayer;



 
    void LateUpdate()
    {

        Vector3 EndPoint = new Vector3(Player.transform.position.x, transform.position.y,  Player.transform.position.z-DistanceFromPlayer); 
        // созраняем в переменную точку  куда должна прийти камера

        transform.position = Vector3.Lerp(transform.position, EndPoint,2f*Time.deltaTime);
        //плавное перемещение  до игрока при его изменении с эффектом запаздывания камеры 


        Quaternion CameraRot = Quaternion.Lerp(transform.rotation, Player.transform.rotation,2f*Time.deltaTime);
        //плавный запаздывающий поворот к игроку


        transform.localRotation = CameraRot;




   }
}
