using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
			//Debug.Log ("collided");
			Destroy (other.gameObject);
	}
}
