using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Guard : HittableHealth
{
    [SerializeField] private float stunDuration = 2f;
    private Coroutine stunCoroutine;
    [SerializeField] private bool canBeStunned = true;
    private EnemyAI enemyScript;
    [SerializeField] private float oldHealth;
    [SerializeField] private GameObject stunEffect;

    private void Start()
    {
        oldHealth = health;
        enemyScript = GetComponent<EnemyAI>();
    }

    public override void Update()
    {
        if(health != oldHealth)
        {
            Stun();
            oldHealth = health;
        }
    }
    
    public void Stun()
    {
        if(stunCoroutine == null && canBeStunned){
            canBeStunned = false;
            stunCoroutine = StartCoroutine(Stun(stunDuration));

        }
        else if(stunCoroutine != null && canBeStunned)
        {
            StopCoroutine(stunCoroutine);
            canBeStunned = false;
            stunCoroutine = StartCoroutine(Stun(stunDuration));
        }
    }

    private IEnumerator Stun(float stunDuration)
    {
        stunEffect.SetActive(true);
        enemyScript.UpdateTarget(this.gameObject);

        yield return new WaitForSeconds(stunDuration);

        enemyScript.UpdateTarget(GetComponent<EnemyAI>().baseTarget);
        StartCoroutine(StunCooldown(stunDuration));
        
    }

    private IEnumerator StunCooldown(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration);
        canBeStunned = true;
        stunEffect.SetActive(false);
    }
}
