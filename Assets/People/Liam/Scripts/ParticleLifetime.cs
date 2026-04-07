using UnityEngine;
using System;
using System.Collections;

public class ParticleLifetime : MonoBehaviour
{
public float Lifetime = 0.3f;
void Start()
    {
        StartCoroutine(DestroyAfterSeconds(Lifetime));
    }

    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
