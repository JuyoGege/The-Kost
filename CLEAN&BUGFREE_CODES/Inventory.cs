using UnityEngine;

public class Inventory : MonoBehaviour
{

    Flashlight fl;

    [Header("Battery")]
    public float batteryCount;
    private float minValue = 60f;
    private float maxValue = 100f;

    [Header("Keys")]
    public float keysCount;

    void Awake()
    {
        fl = FindObjectOfType<Flashlight>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (batteryCount > 0)
            {
                fl.Recharge(Random.Range(minValue, maxValue));
                batteryCount--;
            }
        }
    }
}
