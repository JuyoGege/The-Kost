using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class EnemyController : MonoBehaviour {

	public float lookRadius = 10f;	// Detection range for player
	public Transform[] position ;
	public float MaxTime;// menentukan waktu maksimal dia jalan ke point itu sebelum mengganti point tujuannya ke tempat lain (wandering)
    public float Timer;// Waktu yang berjalan saat dia wandering
    public float Area;// Area point dia wander

    Transform target;	// Reference to the player
	NavMeshAgent agent; // Reference to the NavMeshAgent

	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;//menentukan target yang adalah player 
        agent = GetComponent<NavMeshAgent>(); //dapetin component navmeshagent
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
                //Damage Scripts (dikarenakan tidak ada action damage dan hanya jika bersentuhan maka akan ditaro didalam ontriggerenter)

            }
        } else {
			Timer += Time.deltaTime;  //timernya nambah per detik
            if (Timer >= MaxTime)//kalo timernya sudah melebih maksimal waktu yang diberikan
            {
                Vector3 NewPos = NavMeshWanderer (transform.position, Area);//dia cari point baru dari area sekitar dia yang telah ditentukan
                agent.SetDestination (NewPos); //membuat AI mengarah kesitu
                Timer = 0f;// Mereset timernya menjadi 0, dan looping dan mencari point baru lagi dari posisi baru tersebut jika target tidak dalam radius karena di update
            }

		}

	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){// kalo kena collider tag player
        StartCoroutine (HealthCooldown ()); //memulai damage scripts
        FaceTarget ();	// Make sure to face towards the target
	}

	}
	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			StopAllCoroutines ();//saat tidak bersentuhan (sentuhannnya lepas) maka semua coroutine akan stop

        }

	}


	public IEnumerator HealthCooldown(){//coroutine yang dimaksud dalam damagescript
        Debug.Log (PlayerManager.instance.Health);//hanya mengecek berapa health player sekarang
        PlayerManager.instance.Health -= 1f;// player nyawanya dikurang 1
        yield return new WaitForSeconds (3f);//menunggu 3 detik
        StartCoroutine (HealthCooldown ());
        //memulai coroutine yang sama (agar jika tetap bersentuhan maka player nyawanya berkurang terus setiap 3 detik 
        //dan ketika lepas maka stopcoroutine akan berjalan dan player tidak akan mengalami pengurangan nyawa)
    }

    // Rotate to face the target
    void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;//mencari direksi yang akan di normalize (membuat value menjadi antara 0 dan 1)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));//membuat AI mengarah atau rotate kepada target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);//membuat sebuah slerp agar proses menghadap kepada target lebih smooth
    }

	// Show the lookRadius in editor
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

	Vector3 NavMeshWanderer(Vector3 _Origin, float _Area)//method untuk wander,_area adalah area dan _origin adalah posisi sebelum dia melakukan fungsi ini
    {
		Vector3 Direction = Random.insideUnitSphere * _Area;
        //menentukan direksinya (Random.insideunitsphere berfungsi untuk mengambil titik random pada area sekitar dia, tapi hanya sejauh 1 titik, maka dikali _area agar jangkauannya lebih jauh)
        Direction += _Origin;//menambah posisi tujuan dengan posisi awal untuk menambah jauh jarak wander
        NavMeshHit x;//navmeshhit semacam variable atau fungsi untuk menunjukkan hal apa aja yang berhubungan dengan navmeshagent
        NavMesh.SamplePosition (Direction, out x, _Area, -1);//menentukan apakah direction yang dituju adalah point terdekat atau tidak
        return x.position;//resultnya adalah posisi yang sudah ditentukan oleh navmesh.sampleposition.
    }
}