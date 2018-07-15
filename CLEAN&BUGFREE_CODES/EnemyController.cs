using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class EnemyController : MonoBehaviour {

	public float lookRadius = 10f;	// Detection range for player
	public Transform[] position ;
	public float MaxTime;
	public float Timer;
	public float Area;

	Transform target;	// Reference to the player
	NavMeshAgent agent; // Reference to the NavMeshAgent

	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		// Distance to the target
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the lookRadius
		if (distance <= lookRadius) {
			// Move towards the target
			agent.SetDestination (target.position);

			// If within attacking distance
			if (distance <= agent.stoppingDistance) {
				//Damage Scripts

			}
		} else {
			Timer += Time.deltaTime;
			if (Timer >= MaxTime) {
				Vector3 NewPos = NavMeshWanderer (transform.position, Area);
				agent.SetDestination (NewPos);
				Timer = 0f;
			}

		}

	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
	    StartCoroutine (HealthCooldown ());
		FaceTarget ();	// Make sure to face towards the target
	}

	}
	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			StopAllCoroutines ();

		}

	}


	public IEnumerator HealthCooldown(){
		Debug.Log (PlayerManager.instance.Health);
		PlayerManager.instance.Health -= 1f;
		yield return new WaitForSeconds (5f);
		StartCoroutine (HealthCooldown ());
	}

	// Rotate to face the target
	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	// Show the lookRadius in editor
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

	Vector3 NavMeshWanderer(Vector3 _Origin, float _Area)
	{
		Vector3 Direction = Random.insideUnitSphere * _Area;
		Direction += _Origin;
		NavMeshHit x;
		NavMesh.SamplePosition (Direction, out x, _Area, -1);
		return x.position;
	}

}