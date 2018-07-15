using UnityEngine;

[RequireComponent(typeof(Light))]

public class Flashlight : MonoBehaviour
{
	Inventory iv;
	Light fl;

	[Header("Battery Replenish Rate")]
	public float minValue = 60f;
	public float maxValue = 100f;

	[Header("Flashlight Setting")]

	public float currBattery = 100f;

	[Range(1, 2)]
	public float initialIntensity = 1.2f;

	[Range(0f, 120f)]
	public int drainSpeed = 60; //time needed in second to drain whole currBattery (100% -> 0%)

	void Awake()
	{
		iv = FindObjectOfType<Inventory>();
		fl = GetComponent<Light>();
		fl.intensity = initialIntensity;
		fl.enabled = false;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			if (iv.batteryCount > 0) {
				Recharge (Random.Range (minValue, maxValue));
				iv.batteryCount--;
			}
		}

		UseFlashlight();
	}

	void UseFlashlight()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (currBattery > 0f)
			{
				fl.enabled = !fl.enabled;
			}
		}

		if (fl.enabled)
		{
			currBattery -= (100f / drainSpeed) * Time.smoothDeltaTime;

			if (currBattery >= 40f)
			{
				fl.intensity = initialIntensity;
			}

			else if (currBattery > 0f && currBattery < 40f)
			{
				fl.intensity = (currBattery / 40f) * initialIntensity;
			}

			else if (currBattery <= 0f)
			{
				currBattery = 0f;
				fl.enabled = false;
			}
		}
	}

	public void Recharge(float value)
	{
		currBattery += value;
		if (currBattery > 100f)
		{
			currBattery = 100f;
		}
	}
}