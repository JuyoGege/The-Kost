using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : MonoBehaviour {

    [SerializeField]
    GameObject Target;

    private bool collide;

    void Start()
    {
        collide = false;
    }

    void Update () {
		if(collide && Input.GetKeyDown(KeyCode.E))
        {
            Target.SetActive(true);
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
