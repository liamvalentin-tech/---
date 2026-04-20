using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : HittableHealth
{
    public GameObject target; // Drag your player object here in Inspector
    private NavMeshAgent agent;
    public Animator ChaseAnim;
    [HideInInspector] public Spawner spawner;
    public float PlayerMaxDistance = 10f;
    public float PlayerDistance;
    public float PlayerMinDistance = 1f;
    public bool Wandering = false;
    public bool Chasing = false;
    public float WanderRadius = 10f;
    public float WanderTimer = 5f;
    public bool Attack = false;

    void Start(){

         agent = GetComponent<NavMeshAgent>();
    }

    void Update() 
    {
        WanderTimer -= Time.deltaTime;
        // Continuously set the enemy's destination to the player's position
        PlayerDistance = Vector3.Distance(transform.position, target.transform.position);
        if (target != null && PlayerDistance < PlayerMaxDistance) 
        {
            agent.SetDestination(target.transform.position);
            ChaseAnim.SetTrigger("Chase");
            Wandering = false;
            Chasing = true;
            Attack = false;
        }
        else
        {
       if (WanderTimer <= 0) {
            ChaseAnim.SetTrigger("Wander");
            Wandering = true;
            Chasing = false;
            Vector3 newPos = RandomNavMeshLocation(WanderRadius);
            agent.SetDestination(newPos);
            WanderTimer = 5f;
       }
        }
    }

    public Vector3 RandomNavMeshLocation(float radius) {
        // Pick a random point inside a sphere
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

                // Sample the NavMesh to find the closest valid point
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public override void Death()
    {
        base.Death();
    }
}