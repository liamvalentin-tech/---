using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    private float timer;
    private GameObject target;
    public int curEnemyCount = 0;
    [SerializeField] int maxEnemyCount;


    void Start()
    {
        curEnemyCount = maxEnemyCount;
        GameObject player = FindObjectOfType<FirstPersonMovement>().gameObject;
        target = player;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if(curEnemyCount > 0)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyAI>().target = target;
        }
    }
}
