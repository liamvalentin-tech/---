using UnityEngine;
using System;
using System.Collections;

public class HittableHealth : MonoBehaviour
{
    public GameObject DeathEffect;
    public float health = 100f;
    void Update()
    {
        if (health <= 0f)
        {
            Destroy(gameObject);
            GameObject deathEffect = Instantiate(DeathEffect, transform.position, transform.rotation);
        }
    }
}
