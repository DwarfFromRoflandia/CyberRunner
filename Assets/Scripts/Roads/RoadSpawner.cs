using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;//создаём список дорог
    private float offset = 100f;//переменная, которая отвечает за смещение новой появившейся панели по оси Z

    private void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();//сортируем массив для того, чтобы в нём сохранялся тот порядок элементов, который мы назначили изначально
        }
    }

    public void MoveRoad()
    {
        GameObject moveRoad;//создаём временную переменную в которую присвоим первый элемент из списка, т.е. ту дорогу, которая стоит перед персонажем (объект FirstRoad(1))
        moveRoad = roads[0];
        roads.Remove(moveRoad);//удаляем первый элемент из списка. Т.е. эта строчка позволит удалять те участки дороги, которые игрок пробежал и это благоприятно повлияет на производительность
        float newZ = roads[roads.Count - 1].transform.position.z + offset;//вычисляем новую координату Z на основе положения последнего элемента по оси Z и смещения (переменной offset)
        moveRoad.transform.position = new Vector3(0, 0, newZ);//устанавливаем положение новой появившейся дороге
        roads.Add(moveRoad);//в конец списка добавляем новую появившеюся дорогу
    }

}
