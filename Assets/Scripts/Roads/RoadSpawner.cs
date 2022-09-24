using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;//создаём список дорог
    public GameObject FirstRoad;
    private Queue<GameObject> SpawnedRoads = new Queue<GameObject>(2); // список появившихся дорог
    public Player PlayerCoord;
   [SerializeField] private float offset = 1000f;//переменная, которая отвечает за смещение новой появившейся панели по оси Z
    private Light[] Lights = new Light[5];
    private RoadsType RoadNow;
    private  enum RoadsType
    { 
    CityRoad = 0,
    Draught = 1
    }
     
private void Start()
    {
       
         RoadNow = RoadsType.CityRoad;

        SpawnedRoads.Enqueue(FirstRoad);

        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList(); //сортируем массив для того, чтобы в нём сохранялся тот порядок элементов, который мы назначили изначально
        }
    }

    public void MoveRoad()
    {
    RoadNow = RoadNow.Equals(RoadsType.CityRoad) ? RoadsType.Draught : RoadsType.CityRoad;//меняем тип следующей дороги которая создасться
        
     float newZ = PlayerCoord.transform.position.z + offset;  //вычисляем новую координату Z на основе положения последнего элемента по оси Z и смещения (переменной offset)

     GameObject gm =  Instantiate(roads[(int)RoadNow], new Vector3(0, SpawnedRoads.Last().transform.position.y-0.5f, newZ), Quaternion.identity);//создаем дорогу и сохраняем на нее экземляр

     SpawnedRoads.Enqueue(gm);

    

	if (SpawnedRoads.Count() > 2)
		Destroy(SpawnedRoads.Dequeue());//удаляем начальный элемент очереди

        DestroyLights();
 
        
        






	}
	private void DestroyLights()
	{
        try
        {
            Lights = FindObjectsOfType<Light>();
            /*Юнити создает свет автоматически при добавлении сцены.
             * Оно не распространяется на билд игры но в инспекторе видно.
             * Для перестраховки удаляем лишнее освещение*/
            if (Lights.Length > 2)
            {
                 
                for (int i = 1; i < Lights.Length - 1; i++)
                    Destroy(Lights[i]);
            }
        }
        catch (System.Exception)
        { 
        
        //не вызываем ошибки
        }
        
    }

}

 
