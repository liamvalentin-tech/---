using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public KeyCode Shootbutton = KeyCode.Mouse0;
    public bool canShoot = true;
    public bool IsShooting { get; private set; }
    public GameObject BulletPrefab;
    public GameObject GunBarrel;
    public float Cooldown = 0.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        if (canShoot)
            {
        IsShooting = canShoot && Input.GetKey(Shootbutton);
            GameObject bullet = Instantiate(BulletPrefab, GunBarrel.transform.position, GunBarrel.transform.rotation);
            canShoot = false;
            StartCoroutine(DelaySeconds(Cooldown));
            }
        }
    }
    private IEnumerator DelaySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canShoot = true;
    }
}
