using UnityEngine;

public class ItemPick : MonoBehaviour
{
	Inventory iv;

	private bool collide;

	void Awake()
	{
		iv = FindObjectOfType<Inventory>();
		collide = false;
	}

	void Update()
	{
		if (collide && Input.GetKeyDown(KeyCode.E))
		{
			if (tag == "Battery")
			{
				print("got battery!");
				iv.batteryCount++;
			}
			else if (tag == "Keys")
			{
				print("got keys!");
				iv.keysCount++;
			}

			collide = false;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			collide = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			collide = false;
		}
	}
}

