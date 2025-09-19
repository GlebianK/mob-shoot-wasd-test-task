using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float move_speed;
    [SerializeField] private float rotation_speed = 35.5f;
    [SerializeField] private float damage;

    private GameObject target;
    private Health targetHealth; // В данном проекте таргетом служит лишь игрок, так что сразу сохраняем ссылку на его Health-компонент
    private Vector2 rotateDirection;
    private GameObject parentPool;

    public GameObject ParentPool => parentPool;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerControl>().gameObject;

        if (target == null)
            throw new System.ArgumentNullException($"Object {gameObject.name} can't find player!");

        if (!target.TryGetComponent<Health>(out targetHealth))
            throw new System.ArgumentNullException($"Object {gameObject.name} can't find player's health!");
    }


    private void Update()
    {
        RotateEnemyTowardsTarget();
        MoveEnemyTowardsTarget();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log($"Enemy ({gameObject.name}) hit {col.gameObject.name}!");

        // Подход с учётом единственности игрока как цели
        if (col.gameObject.CompareTag("Player") && targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        /*
         * Более книверсальный подход
        if (col.gameObject.CompareTag("Player") && col.gameObject.TryGetComponent<Health>(out Health playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }
        */
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
    }

    private void RotateEnemyTowardsTarget()
    {
        if (target != null)
        {
            rotateDirection = (target.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(rotateDirection, transform.up);
            Quaternion rotationQuaternion = Quaternion.RotateTowards(transform.rotation, targetRotation, rotation_speed * Time.deltaTime);
            rb.SetRotation(rotationQuaternion);
        }
    }

    private void MoveEnemyTowardsTarget()
    {
        rb.linearVelocity = rotateDirection * (move_speed * Time.deltaTime);
    }

    public void SetParentPool(GameObject poolToSet)
    {
        parentPool = poolToSet;
    }

    public void OnDied() // callback для Died-события компонента Health
    {
        GameManager.Instance.CountKills(1);
        /*
        if (parentPool.TryGetComponent<EnemySpawner>(out EnemySpawner enemySpawner))
        {
            enemySpawner.ReturnEnemyToPool(gameObject);
        }*/
        Destroy(gameObject);
    }
}
