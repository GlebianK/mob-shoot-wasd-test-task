using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float move_speed;
    [SerializeField] private float damage;

    private GameObject target;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerControl>().gameObject;

        if (target == null)
            throw new System.ArgumentNullException($"Object {gameObject.name} can't find player!");
    }


    private void Update()
    {
        MoveEnemyToTarget();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Enemy ({gameObject.name}) hit {col.gameObject.name}!");
        //TODO get health component
    }

    private void MoveEnemyToTarget()
    {
        //TODO двигать врага к игроку
    }
}
