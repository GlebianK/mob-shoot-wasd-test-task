using UnityEngine;

public class GunMultiple : GunBase
{
    [SerializeField] private int projectilesPerShot = 3;

    protected override GameObject TakeFromPool()
    {
        if (projectilePool.Count < projectilesPerShot)
            return null;

        GameObject temp = projectilePool.Dequeue();
        Debug.Log($"GunSingle: Took projectile from pool! Pool size: {projectilePool.Count}");
        return temp;
    }

    public override void Shoot()
    {
        if (!canShoot)
            return;

        Debug.LogWarning("ÁÀÌ, ÁË_ÒÜ!");
        GameObject temp = TakeFromPool();

        if (temp == null)
            return;

        temp.SetActive(true);
        if (temp.TryGetComponent<Projectile>(out Projectile projectileComponent))
            projectileComponent.FireProjectile();
        else
            Debug.LogError("Gun -> Shoot -> No projectile component for current projectile!");

        StartCoroutine(ShootCooldown());
    }
}
