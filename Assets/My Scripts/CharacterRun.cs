using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRun : MonoBehaviour {
	public float speed;
	private Rigidbody2D rb;
	public float delaytime;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		Accelerate ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = transform.right*speed;
	}

	public void Accelerate(){
		StartCoroutine(delay ());
	}

	IEnumerator delay(){
		yield return new WaitForSeconds (delaytime);
		Time.timeScale += 0.1f;
		Accelerate ();
		Debug.Log (Time.timeScale);
	}
}
