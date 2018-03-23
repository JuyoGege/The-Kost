using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour {

	public GameObject[] Blocks;
	public Transform SpawnPos;
	public int BlockCount = 0;
	public float NewPos = 4.0f;

	private int randomNum;
	private float waitTime = 0.8f;
	private GameObject block;

	void Start ()
	{
		Block ();
	}
	void update ()
	{
	}
	public void Block()
	{
		block = Instantiate (Blocks [Random.Range (0, 4)], SpawnPos.position, Quaternion.identity) as GameObject;
		Vector3 temp = SpawnPos.position;
		temp .y = 0;
		temp.x += 4;
		temp.z = 0;
		SpawnPos.position = temp;
		StartCoroutine (wait ());
	}
	IEnumerator wait ()
	{
		yield return new WaitForSeconds(waitTime);
		BlockCount += 1;
		Block();
	}

}
