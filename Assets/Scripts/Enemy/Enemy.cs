using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy  :MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]private float EnemyDamage = 0.2f;//урон передаваемый врагои
 
	public void OnCollisionEnter(Collision collision)
	{
		 
		if (collision.gameObject.CompareTag("Player"))
		{
			 
			EventManager.IsPunched.Invoke(EnemyDamage);

		}
	}
}
