using UnityEngine;

[RequireComponent(typeof(Light))]

public class Flashlight : MonoBehaviour
{

    [SerializeField]
    float initialIntensity = 1.2f;

    [Range(0f, 10f)]
    [SerializeField]
    float drainSpeed = 1.66f;

    [Range(0f, 100f)]
    [SerializeField]
    float battery = 100f;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector3 offset;

    private Light flashlight;

    private bool collide;
    private bool held;

    void Start()
    {
        collide = false;
        held = false;
        flashlight = gameObject.GetComponent<Light>();
        flashlight.type = LightType.Spot;
        flashlight.range = 15f;
        flashlight.intensity = initialIntensity;
        flashlight.spotAngle = 60f;
        flashlight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //collide
        {
            transform.position = player.transform.position + offset;
            transform.rotation = player.transform.rotation;
            transform.parent = player.transform;
            transform.position.Normalize();
            Destroy(GetComponent<Collider>());
            collide = false;
            held = true;
        }

        if (held && Input.GetKeyDown(KeyCode.F))
        {
            if (battery > 0f)
            {
                flashlight.enabled = !flashlight.enabled;
            }
        }

        if (flashlight.enabled)
        {
            battery -= drainSpeed * Time.smoothDeltaTime;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collide = true;
        }
    }
}