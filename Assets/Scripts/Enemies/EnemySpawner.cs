using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    [Header("Spawner Settings")]
    public float range = 10f;
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
    public int spawnAmount = 1;

    private int currentEnemies = 0;
    private float counter;
    private bool enableSpawner;

    private void Start()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        col.radius = range;
        col.isTrigger = true;
    }

    private void Update()
    {
        if (!enableSpawner) return;

        counter += Time.deltaTime;

        if (counter >= spawnInterval)
        {
            SpawnEnemies();
            counter = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enableSpawner = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enableSpawner = false;
        }
    }

    void SpawnEnemies()
    {
        if (currentEnemies >= maxEnemies) return;

        for (int i = 0; i < spawnAmount; i++)
        {
            if (currentEnemies >= maxEnemies) break;

            Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            Vector3 pos = transform.position + dir * Random.Range(0, range);

            GameObject enemy = Instantiate(EnemyPrefab, pos, Quaternion.identity);

            BaseEnemy be = enemy.GetComponent<BaseEnemy>();
            if (be != null)
                be.spawner = this;

            currentEnemies++;
        }
    }

    public void RemoveEnemy()
    {
        currentEnemies--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}