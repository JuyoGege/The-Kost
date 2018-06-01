using UnityEngine;
using System.Collections;

public class DoorScripts : MonoBehaviour {
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
	}

	public void DoorOpens()
	{
		anim.SetBool ("Opens", true);
	}
	public void DoorCloses()
	{
		anim.SetBool ("Opens", false);
	}
	void Collenable()
	{
		GetComponent<Collider2D>().enabled = true;
	}

	void CollDisable()
	{
		GetComponent<Collider2D>().enabled = false;
	}
}