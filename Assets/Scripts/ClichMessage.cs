using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClichMessage :MonoBehaviour{
	// Start is called before the first frame update
	private Player Player;
	private void Start()
	{
		Player = GameObject.Find("Player").GetComponent<Player>();
	}
 
	public void Clic()
	{
		 Player.MovePerson(Input.mousePosition);
		print("Áûכמ");
	}

}
