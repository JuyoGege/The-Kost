using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMvmt : MonoBehaviour {
	public Transform Player;
	int moveSpeed = 3;
	int maxDist =4;
	int minDist =4;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Player);
		//Mengikut Player Seperti Pet
		if (Vector3.Distance (transform.position, Player.position) >= minDist) {
			transform.position += transform.forward * moveSpeed * Time.deltaTime;

			if (Vector3.Distance (transform.position, Player.position) <= maxDist) {
				
			}
		}

	}}
