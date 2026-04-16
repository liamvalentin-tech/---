using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : HittableHealth
{
    public GameObject target; // Drag your player object here in Inspector
    private NavMeshAgent agent;
    public Animator ChaseAnim;
    [HideInInspector] public Spawner spawner;

    void Start(){

         agent = GetComponent<NavMeshAgent>();
    }

    void Update() 
    {
        // Continuously set the enemy's destination to the player's position
        if (target != null) 
        {
            agent.SetDestination(target.transform.position);
            ChaseAnim.SetTrigger("Chase");
        }
    }

    public override void Death()
    {
        base.Death();
        spawner.curEnemyCount--;
    }
}