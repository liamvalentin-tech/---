using UnityEngine;
using System;
using System.Collections;

public class HittableHealth : MonoBehaviour
{
    public GameObject DeathEffect;
    public float health = 100f;
    public virtual void Update()
    {
        if (health <= 0f)
        { Death();
        }
    }
    public virtual void Death(){
         Destroy(gameObject);
        GameObject deathEffect = Instantiate(DeathEffect, transform.position, transform.rotation);
    }
}
