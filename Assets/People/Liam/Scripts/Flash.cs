using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour
{
    public float LifeTimeFlash = 0.1f;
    void Update()
    {
        StartCoroutine(Flashingthing(LifeTimeFlash));
    }
    private IEnumerator Flashingthing(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
