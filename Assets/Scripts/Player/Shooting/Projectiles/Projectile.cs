using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 shootDirection;
    [SerializeField] private Vector3 basicScale = new Vector3(0.5f, 0.5f, 0.5f);

    private GameObject parentGun;

    public float Damage => damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.LogWarning($"Projectile.cs: Hit!");
        if (!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("Projectile"))
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
        }        
    }

    public void FireProjectile()
    {
        transform.parent = null;
        transform.localScale = basicScale;
        rb.AddRelativeForce(shootDirection * speed);
    }

    public void SetParentGun(GameObject gun)
    {
        parentGun = gun;
    }

    public void SetShootDirection(Vector2 direction)
    {
        shootDirection = direction;
    }

}
