using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public float LifeTime = 3f;

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        StartCoroutine(DestroyAfterSeconds(LifeTime));
    }
    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
