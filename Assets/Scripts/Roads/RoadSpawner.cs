using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;//создаЄм список дорог
    public Player PlayerCoord;
   [SerializeField] private float offset = 1000f;//переменна€, котора€ отвечает за смещение новой по€вившейс€ панели по оси Z
    private int num = 0;
    private int NumRoad
    #region    

    {
        get
        {
            NumRoad = num;
            return num; 
        
        } 
        set

        {

            if (value == 1)num = 0;
            
            else if (value == 0) num = 1;
       }


    }
    #endregion  
private void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList(); //сортируем массив дл€ того, чтобы в нЄм сохран€лс€ тот пор€док элементов, который мы назначили изначально
        }
    }

    public void MoveRoad()
    {
         

        print("Ќова€ дорога должна по€витс€ " + NumRoad);

        GameObject NewRoad; 
        //создаЄм временную переменную в которую присвоим первый элемент из списка, т.е. ту дорогу, котора€ стоит перед персонажем (объект FirstRoad(1))
        NewRoad = roads[NumRoad];
      
        float newZ = PlayerCoord.transform.position.z + offset;  //вычисл€ем новую координату Z на основе положени€ последнего элемента по оси Z и смещени€ (переменной offset)

      Instantiate(NewRoad, new Vector3(0, 0, newZ), Quaternion.identity);

        if (NumRoad.Equals(1))
            Destroy(NewRoad);
         


       





    }

}
