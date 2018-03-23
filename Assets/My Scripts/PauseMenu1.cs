using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu1 : MonoBehaviour {
	public string levelToLoad;
	public bool isPaused = false;
	public GameObject pauseMenuCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
		} else {
			pauseMenuCanvas.SetActive (false);
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			isPaused = !isPaused;
		}
		
	}

	public void Resume(){
		isPaused = false;
	}

	public void MainMenu(){
		Application.LoadLevel (levelToLoad);
	}

	public void Quit(){
		Application.Quit ();
	}
}
