using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public List<GameObject> Enemys;
	GameObject EnemyLeft;
	private void Start()
	{
		 
		EnemyLeft = Instantiate(Enemys[Random.Range(0, 4)],
					   transform.position - new Vector3(70, 0, 0),
					   Quaternion.Euler(0, 180, 0));
		StartCoroutine(StartSpawn());
	}
	bool switcher = true;
	public IEnumerator StartSpawn()
	{

		while (true)
		{


	 
			if (Vector3.Distance(gameObject.transform.position, EnemyLeft.transform.position) >= 1000)
			{
				switch (switcher)
				{
					case true:

				EnemyLeft = Instantiate(Enemys[Random.Range(0, 4)],
					  transform.position - new Vector3(70, 0, 0),
					  Quaternion.Euler(0, 180, 0));
						 switcher = false;
						break;

					case false:
				EnemyLeft = Instantiate(Enemys[Random.Range(0, 4)],
					  transform.position + new Vector3(70, 0, 0),
					  Quaternion.Euler(0, 180, 0));
						 switcher = true;
						break;
				}
			} 

		}
			yield return null;

	}


	
}
