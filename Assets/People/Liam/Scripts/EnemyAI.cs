using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
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
    public bool Attack = false;
    public float WanderRadius = 10f;
    public float WanderTimer = 5f;
    public float speed;

    void Start(){
        target = GameObject.FindGameObjectWithTag("Targettable");
         agent = GetComponent<NavMeshAgent>();
    }

    void Update() 
    {
        WanderTimer -= Time.deltaTime;
        // Continuously set the enemy's destination to the player's position
        PlayerDistance = Vector3.Distance(transform.position, target.transform.position);
        if (target != null && PlayerDistance < PlayerMaxDistance) 
        {
            if (PlayerDistance <= PlayerMinDistance) {
                //agent.SetDestination(transform.position);
                //transform.LookAt(target.transform, Vector3.forward);
                //agent.
                //ChaseAnim.SetTrigger("Attack");
                Attack = true;
                Wandering = false;
                Chasing = false;
            }
            else
            {
                //ChaseAnim.SetTrigger("Chase");
                Attack = false;
                Chasing = true;
                Wandering = false;
                agent.SetDestination(target.transform.position);
            }
        }
        else
        {
       if (WanderTimer <= 0) {
            //ChaseAnim.SetTrigger("Wander");
            Wandering = true;
            Chasing = false;
            Attack = false;
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
}