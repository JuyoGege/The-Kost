using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
	public float TimeSpeed;
	public float maxTime;
	public int delayTime;
	public float increment;

	// Use this for initialization
	void Start () {
		Time.timeScale = TimeSpeed;
		StartCoroutine (delay());
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	public void ChangeTime(){
		Time.timeScale = TimeSpeed;
		Time.timeScale += increment;
		TimeSpeed = Time.timeScale;
		StartCoroutine (delay());
		//Debug.Log (Time.timeScale);
	}
	IEnumerator delay(){
		yield return new WaitForSeconds (delayTime);
		if(Time.timeScale <= maxTime) 
			ChangeTime();
	}

	public void pause(){
		StopCoroutine (delay());
		Time.timeScale = 1f;
	}
}
