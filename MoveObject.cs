using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.tag == "Arrow")
			transform.Translate (-Vector2.right * Time.deltaTime * 0.5f);
		else if(gameObject.tag == "Gate")
			transform.Translate (-Vector2.up * Time.deltaTime * 0.5f);
		else
			transform.Translate (-Vector2.right * Time.deltaTime *2);
		//if(gameObject.tag == "Gate")
			//transform.Translate (-Vector2.up * Time.deltaTime *2);
	}
}
