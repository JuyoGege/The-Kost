using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideLaser : MonoBehaviour {
	[SerializeField]
	GameObject gameOver;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Debug.Log ("collided");
			gameOver.GetComponent<GameOverMenu> ().isDead = true;
		}
	}
	 
}
