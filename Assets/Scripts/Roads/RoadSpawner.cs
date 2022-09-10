using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;//создаЄм список дорог
    public GameObject FirstRoad;
    private Queue<GameObject> SpawnedRoads = new Queue<GameObject>(2); // список по€вившихс€ дорог
    public Player PlayerCoord;
   [SerializeField] private float offset = 1000f;//переменна€, котора€ отвечает за смещение новой по€вившейс€ панели по оси Z
    private Light[] Lights = new Light[2];
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
            roads = roads.OrderBy(r => r.transform.position.z).ToList(); //сортируем массив дл€ того, чтобы в нЄм сохран€лс€ тот пор€док элементов, который мы назначили изначально
        }
    }

    public void MoveRoad()
    {
    RoadNow = RoadNow.Equals(RoadsType.CityRoad) ? RoadsType.Draught : RoadsType.CityRoad;//мен€ем тип следующей дороги котора€ создастьс€
        
     float newZ = PlayerCoord.transform.position.z + offset;  //вычисл€ем новую координату Z на основе положени€ последнего элемента по оси Z и смещени€ (переменной offset)

     GameObject gm =  Instantiate(roads[(int)RoadNow], new Vector3(0, SpawnedRoads.Last().transform.position.y-0.5f, newZ), Quaternion.identity);//создаем дорогу и сохран€ем на нее экземл€р

     SpawnedRoads.Enqueue(gm);

    print(SpawnedRoads.Count());

	if (SpawnedRoads.Count() > 2)
		Destroy(SpawnedRoads.Dequeue());//удал€ем начальный элемент очереди


    /*ёнити создает свет автоматически при добавлении сцены.
     * ќно не распростран€етс€ на билд игры но в инспекторе видно.
     * ƒл€ перестраховки удал€ем лишнее освещение*/
        Lights = FindObjectsOfType<Light>();          
        Destroy(Lights[1]);
        
        






	}

}

 
