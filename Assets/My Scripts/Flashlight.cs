using UnityEngine;

[RequireComponent(typeof(Light))]

public class Flashlight : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField]
    float initialIntensity = 1.2f;

    [SerializeField]
    float battery = 100f;

    [Range(0f, 120f)]
    [SerializeField]
    int drainSpeed = 60; //time needed in second to drain whole battery (100%)

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    Vector3 offset;

    private Light flashlight;

    private bool collide;
    private bool held;

    void Start()
    {
        flashlight = gameObject.GetComponent<Light>();
        flashlight.type = LightType.Spot;
        flashlight.range = 15f;
        flashlight.intensity = initialIntensity;
        flashlight.spotAngle = 60f;
        flashlight.enabled = false;
        collide = false;
        held = false;
    }

    void Update()
    {
        if (collide && Input.GetKeyDown(KeyCode.E))
        {
            transform.parent = playerCamera.transform;
            transform.position = player.transform.position + offset;
            transform.rotation = playerCamera.transform.rotation;
            Destroy(GetComponent<Collider>());
            held = true;
        }

        if (held)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (battery > 0f)
                {
                    flashlight.enabled = !flashlight.enabled;
                }
            }
        }

        if (flashlight.enabled)
        {
            battery -= (100f / drainSpeed) * Time.smoothDeltaTime;

            if (battery >= 40f)
            {
                flashlight.intensity = initialIntensity;
            }

            else if (battery > 0f && battery < 40f)
            {
                flashlight.intensity = (battery / 40f) * initialIntensity;
            }

            else if (battery <= 0f)
            {
                battery = 0f;
                flashlight.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collide = true;
        }
    }
}