using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectile;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 shootDirection;

    private float damage;
    private float speed;

    private GameObject parentGun;
    private GameObject parentFirePoint;

    public float Damage => damage;

    private void Awake()
    {
        damage = projectile.projectileDamage;
        speed = projectile.projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.LogWarning($"Projectile.cs: Hit!");
        if (!col.gameObject.CompareTag("Player"))
        {
            //Debug.Log($"Projectile {gameObject.name} hit smth! (smth is {col.gameObject.name})");
            if (col.gameObject.TryGetComponent<Health>(out Health targetHealth))
            {
                targetHealth.TakeDamage(damage);
            }

            if (parentGun.TryGetComponent<GunBase>(out GunBase gunComponent))
                gunComponent.ReturnToPool(gameObject);
            else
                Debug.LogError("Projectile -> OnTrigEn2d -> No parent gun or Gun component on it!");

            transform.parent = parentFirePoint.transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }        
    }

    public void FireProjectile()
    {
        transform.parent = null;
        rb.AddRelativeForce(shootDirection * speed);
    }

    public void SetParentGun(GameObject gun, GameObject gunFirePoint)
    {
        parentGun = gun;
        parentFirePoint = gunFirePoint;
    }

}
