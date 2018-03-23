using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDoor : MonoBehaviour {

	public GameObject player;
	public Transform target;

	void Start () {
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
		{
			player.transform.position = target.transform.position;
		}
	}
		

}
