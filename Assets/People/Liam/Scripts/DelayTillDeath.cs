using UnityEngine;
using System.Collections;

public class DelayTillDeath : MonoBehaviour
{
    public float delay = 1f;

    private void Start()
    {
        StartCoroutine(DeathDelay());
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
