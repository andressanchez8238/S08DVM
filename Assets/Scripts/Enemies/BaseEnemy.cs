using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float range = 8f;
    public float speed = 3f;

    private Transform player;
    private bool playerInRange;

    public EnemySpawner spawner;

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        playerInRange = dist <= range;

        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;

            // Si lo toca, se destruye p
            Destroy(gameObject);

            if (spawner != null)
                spawner.RemoveEnemy();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        if (playerInRange && player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}