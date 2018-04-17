using UnityEngine;

public class Battery : MonoBehaviour
{
    Inventory it;

    [SerializeField]
    GameObject Player;

    private bool collide;

    void Start()
    {
        it = Player.GetComponent<Inventory>();
        collide = false;
    }

    void Update()
    {
        if (collide && Input.GetKeyDown(KeyCode.E))
        {
            it.batteryCount++;
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
}
