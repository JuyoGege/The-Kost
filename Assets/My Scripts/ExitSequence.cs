using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSequence : MonoBehaviour {
	[SerializeField]
	GameObject doorOpen;

	[SerializeField]
	GameObject doorClose;

	public bool isOpen = false;

	void Start (){
		gameObject.GetComponent<SpriteRenderer>().sprite = doorClose.GetComponent<SpriteRenderer>().sprite;
	}

	public void OpenDoor (){
		gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen.GetComponent<SpriteRenderer>().sprite;
	}

}
