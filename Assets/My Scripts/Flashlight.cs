using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
	public Light flashlight;
	public bool activateFlashlight;
	public float flashlightBattery;
	public bool reduceIntensity;

	void Start(){
		flashlight = GetComponent<Light> ();
	}
	void Update(){
		if (flashlightBattery <= 0) {
			flashlightBattery = 0;
		}
		flashlight.enabled = activateFlashlight;
		if (Input.GetKeyDown ("f")) {
			activateFlashlight = !activateFlashlight;
			}
		if (flashlightBattery != 0) {
			if (activateFlashlight) {
				flashlightBattery -= Time.time * 0.1f / 10;
			}
		}
		if (flashlightBattery <= 80) {
			reduceIntensity = true;
		} else if (flashlightBattery >= 80) {
			reduceIntensity = false;
		}

		if (reduceIntensity) {

			flashlight.intensity = flashlightBattery / 10;
		}
	}
}
