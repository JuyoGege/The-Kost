using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlower : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			GameObject.Find ("TimeManager").GetComponent<TimeManager> ().pause ();
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			GameObject.Find ("TimeManager").GetComponent<TimeManager> ().pause ();
		}
	}
}
