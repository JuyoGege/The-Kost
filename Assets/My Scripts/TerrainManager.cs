using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

	private Vector3 start; //= new Vector3 (-9.05f, -5.17f, -1 f);
	public float n, x, y, z;
	public float delaytime;
	public GameObject floor;

	// Use this for initialization
	void Start () {
		n = x;
		MakeFloor (0);
	}
	
	// Update is called once per frame
	void Update () {
		TileKiller ();
	}
	public void MakeFloor(int i){
		n += i;
		//for(int i=0;i<11;i++) {
			Instantiate (floor, new Vector3(n,y,z), transform.rotation);
		//}
		StartCoroutine (delay());
	}

	public void TileKiller(){
//		if (GameObject.Find ("TileKiller").GetComponent<Collider2D> ().) {
		
		//}
	}

	IEnumerator delay(){
		yield return new WaitForSeconds (delaytime);
		MakeFloor(2);
	}
}
