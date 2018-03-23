using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    //script untuk darah player
    public float fullHealth;
    float currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = fullHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            makeDead();
        }
    }
    public void makeDead()
    {
        Destroy(gameObject);
    }
}