using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	[SerializeField]
	GameObject switchstate;

	[SerializeField]
	GameObject laserstate1;

	[SerializeField]
	GameObject laserstate2;

	[SerializeField]
	GameObject laserstate3;

	[SerializeField]
	GameObject exitDoor;

	[SerializeField]
	GameObject laserDie1;

	[SerializeField]
	GameObject laserDie2;

	[SerializeField]
	GameObject laserDie3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (switchstate.GetComponent<SwitchDoor> ().isOn) {
			exitDoor.GetComponent<ExitSequence> ().OpenDoor ();
			exitDoor.GetComponent<loadLevel> ().isUnlock = true;
		}

		if (!laserstate3.GetComponent<SwitchLaser1> ().isOn)
			laserDie3.GetComponent<LaserSequence> ().OffLaser ();
		if (!laserstate1.GetComponent<SwitchLaser1> ().isOn)
			laserDie1.GetComponent<LaserSequence> ().OffLaser ();
		if (!laserstate2.GetComponent<SwitchLaser1> ().isOn)
			laserDie2.GetComponent<LaserSequence> ().OffLaser ();
		
	}
}
