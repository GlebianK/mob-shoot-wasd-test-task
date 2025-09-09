using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectile;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 shootDirection;

    private float damage;
    private float speed;

    private GameObject parentGun;

    public float Damage => damage;

    private void Awake()
    {
        damage = projectile.projectileDamage;
        speed = projectile.projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.LogWarning($"Trigger collision with {col.gameObject.name}");
        if (col.gameObject.CompareTag("Enemy"))
        {
            // TODO: добавить отнятие здоровья
            Debug.LogWarning($"Projectile {gameObject.name} hit smth! (smth is {col.gameObject.name})");
        }

        if (parentGun.TryGetComponent<Gun>(out Gun gunComponent))
            gunComponent.ReturnToPool(gameObject);
    }

    public void FireProjectile()
    {
        rb.AddRelativeForce(shootDirection * (speed* Time.deltaTime));
    }

    public void SetParentGun(GameObject gun)
    {
        parentGun = gun;   
    }

}
