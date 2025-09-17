using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float move_speed;
    [SerializeField] private float rotation_speed = 35.5f;
    [SerializeField] private float damage;

    private GameObject target;

    private Vector3 rotateDirection;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerControl>().gameObject;

        if (target == null)
            throw new System.ArgumentNullException($"Object {gameObject.name} can't find player!");
    }


    private void Update()
    {
        RotateEnemyTowardsTarget();
        MoveEnemyTowardsTarget();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Enemy ({gameObject.name}) hit {col.gameObject.name}!");

        //TODO get health component
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
}
