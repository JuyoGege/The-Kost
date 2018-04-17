using UnityEngine;

[RequireComponent(typeof(Light))]

public class Flashlight : MonoBehaviour
{
    Light fl;

    [Range(1, 2)]
    [SerializeField]
    float initialIntensity = 1.2f;

    [SerializeField]
    float battery = 100f;

    [Range(0f, 120f)]
    [SerializeField]
    int drainSpeed = 60; //time needed in second to drain whole battery (100% -> 0%)

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    Vector3 offset;

    void Start()
    {
        fl = gameObject.GetComponent<Light>();
        fl.intensity = initialIntensity;
        fl.enabled = false;
    }

    void Update()
    {
        UseFlashlight();
    }

    void UseFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (battery > 0f)
            {
                fl.enabled = !fl.enabled;
            }
        }

        if (fl.enabled)
        {
            battery -= (100f / drainSpeed) * Time.smoothDeltaTime;

            if (battery >= 40f)
            {
                fl.intensity = initialIntensity;
            }

            else if (battery > 0f && battery < 40f)
            {
                fl.intensity = (battery / 40f) * initialIntensity;
            }

            else if (battery <= 0f)
            {
                battery = 0f;
                fl.enabled = false;
            }
        }
    }

    public void Recharge(float value)
    {
        battery += value;
        if (battery > 100f)
        {
            battery = 100f;
        }
    }
}