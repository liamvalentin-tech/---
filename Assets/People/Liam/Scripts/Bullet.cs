using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public float LifeTime = 3f;
    public float damage = 10f;

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
    private void OnTriggerEnter(Collider collision)
    {
        HittableHealth health = collision.gameObject.GetComponent<HittableHealth>();
        if (health != null)
        {
            health.health -= damage;
        }
    if (collision.gameObject.CompareTag("Hittable"))
        {
        Destroy(gameObject);
    }
}
}
