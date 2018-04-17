using UnityEngine;

public class Inventory : MonoBehaviour {

    Flashlight fl;

    [SerializeField]
    GameObject Flashlight;

    [SerializeField]
    float minValue = 60f;

    [SerializeField]
    float maxValue = 100f;

    public float batteryCount;

    // Use this for initialization
    void Start () {
        fl = Flashlight.GetComponent<Flashlight>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            if (batteryCount > 0)
            {
                fl.Recharge(Random.Range(minValue, maxValue));
                batteryCount--;
            }
    }
}
