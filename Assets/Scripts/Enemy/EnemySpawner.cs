using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public List<GameObject> Enemys;
	GameObject Enemy;
	private void Start()
	{
		 
		Enemy = Instantiate(Enemys[Random.Range(0, Enemys.Count)],
					   transform.position - new Vector3(70, 0, 0),
					   Quaternion.Euler(0, 180, 0));
		StartCoroutine(StartSpawn());
	}
	bool switcher = true;
	public IEnumerator StartSpawn()
	{
if (Enemy != null)

			while (true)
		{


 
			if (Vector3.Distance(gameObject.transform.position, Enemy.transform.position) >= 500)
			{
				float changeX;
				switch (switcher)
				{
					case true:
						changeX = 70;
						switcher = false;

						break;

					case false:
						changeX = -70;
						switcher =true;
						break;
				}


				Enemy = Instantiate(Enemys[Random.Range(0, Enemys.Count)],
					  transform.position - new Vector3(changeX, 0, 0),
					  Quaternion.Euler(0, 180, 0));
				 
			} 
	 
	yield return null;
		}
else
	StopCoroutine(StartSpawn());


	}


	
}
