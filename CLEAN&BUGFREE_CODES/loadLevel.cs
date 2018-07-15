using UnityEngine;
using System.Collections;

// Buat ngeload level yang baru
public class loadLevel : MonoBehaviour {

	private bool playerInZone;
	public bool isUnlock = false;
	public string levelToLoad;
	// Use this for initialization
	void Start () {
		playerInZone = false;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) && playerInZone && isUnlock)
		{
			Application.LoadLevel(levelToLoad);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			playerInZone = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.name == "Player")
		{
			playerInZone = false;
		}
	}
}