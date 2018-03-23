using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLaser1 : MonoBehaviour {
	[SerializeField]
	GameObject switchOn;

	[SerializeField]
	GameObject switchOff;

	public bool isOn = true;

	void Start (){
		gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
	}


	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E)){
			gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
			isOn = false;
		}
	}
}
