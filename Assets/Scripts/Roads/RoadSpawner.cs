using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;//������ ������ �����
    public GameObject FirstRoad;
    private Queue<GameObject> SpawnedRoads = new Queue<GameObject>(2); // ������ ����������� �����
    public Player PlayerCoord;
   [SerializeField] private float offset = 1000f;//����������, ������� �������� �� �������� ����� ����������� ������ �� ��� Z
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
            roads = roads.OrderBy(r => r.transform.position.z).ToList(); //��������� ������ ��� ����, ����� � �� ���������� ��� ������� ���������, ������� �� ��������� ����������
        }
    }

    public void MoveRoad()
    {
    RoadNow = RoadNow.Equals(RoadsType.CityRoad) ? RoadsType.Draught : RoadsType.CityRoad;//������ ��� ��������� ������ ������� ����������
        
     float newZ = PlayerCoord.transform.position.z + offset;  //��������� ����� ���������� Z �� ������ ��������� ���������� �������� �� ��� Z � �������� (���������� offset)

     GameObject gm =  Instantiate(roads[(int)RoadNow], new Vector3(0, SpawnedRoads.Last().transform.position.y-0.5f, newZ), Quaternion.identity);//������� ������ � ��������� �� ��� ��������

     SpawnedRoads.Enqueue(gm);

    

	if (SpawnedRoads.Count() > 2)
		Destroy(SpawnedRoads.Dequeue());//������� ��������� ������� �������

        DestroyLights();
 
        
        






	}
	private void DestroyLights()
	{
        try
        {
            Lights = FindObjectsOfType<Light>();
            /*����� ������� ���� ������������� ��� ���������� �����.
             * ��� �� ���������������� �� ���� ���� �� � ���������� �����.
             * ��� ������������� ������� ������ ���������*/
            if (Lights.Length > 2)
            {
                 
                for (int i = 1; i < Lights.Length - 1; i++)
                    Destroy(Lights[i]);
            }
        }
        catch (System.Exception)
        { 
        
        //�� �������� ������
        }
        
    }

}

 
