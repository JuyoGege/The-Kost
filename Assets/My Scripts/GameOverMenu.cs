using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour {
	public string mainMenu,currentLevel;
	public bool isDead;
	public GameObject gameOverCanvas;

	void Start(){
		isDead = false;
	}
	void Update () {
		if (isDead) {
			gameOverCanvas.SetActive (true);
		} else {
			gameOverCanvas.SetActive (false);
		}

	}
		
	public void MainMenu(){
		Application.LoadLevel (mainMenu);
	}

	public void Load(){
		Application.LoadLevel (currentLevel);
	}
}
