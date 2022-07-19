using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private RoadSpawner roadSpawner;

    private void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
    }

    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad();
    }
}
