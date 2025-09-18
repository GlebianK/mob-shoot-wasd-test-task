using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 shootDirection;

    

    private GameObject parentGun;
    private GameObject parentFirePoint;

    public float Damage => damage;

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

            transform.localEulerAngles = Vector3.zero;
            transform.parent = parentFirePoint.transform;
            transform.localPosition = Vector3.zero;
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

    public void SetShootDirection(Vector2 direction)
    {
        shootDirection = direction;
    }

}
