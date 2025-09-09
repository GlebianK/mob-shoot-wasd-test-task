using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GunType gunConfig;
    [SerializeField] private int projectilePoolSize = 10;

    private string gunName;
    private GunType.ShootType shootType;
    private float cooldownBetweenShots;
    private GameObject projectile;

    private Queue<GameObject> projectilePool;

    private GameObject TakeFromPool()
    {
        if (projectilePool.Count < 1)
            return null;

        GameObject temp = projectilePool.Peek();
        Debug.Log($"Took projectile from pool! Pool size: {projectilePool.Count}");
        temp.SetActive(true);
        return temp;
    }

    public void InitializeGun()
    {
        gunName = gunConfig.gunName;
        shootType = gunConfig.gunShootType;
        cooldownBetweenShots = gunConfig.gunCooldownBetweenShots;
        Debug.LogWarning("Gun initialized!");
    }

    public void InitializeProjectiles()
    {
        projectile = gunConfig.gunProjectilePrefab;

        if (projectile != null)
        {
            for (int i = 0; i < projectilePoolSize; i++)
            {
                GameObject temp = Instantiate(projectile, firePoint.transform, true);
                projectilePool.Enqueue(temp);

                if (temp.TryGetComponent<Projectile>(out Projectile projectileComponent))
                {
                    projectileComponent.SetParentGun(gameObject);
                }

                temp.SetActive(false);
            }
            Debug.LogWarning("Projectiles initialized!");
        }
        else
            Debug.LogError($"NO PROJECTILES FOR THE GUN! Gun name: {gameObject.name}");
    }

    public void Shoot()
    {
        Debug.LogWarning("¡¿Ã, ¡À_“‹!");
        GameObject temp = TakeFromPool();
         if (temp.TryGetComponent<Projectile>(out Projectile projectileComponent))
            projectileComponent.FireProjectile();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        projectilePool.Enqueue(objectToReturn);
        objectToReturn.SetActive(false);
        Debug.Log($"Returned projectile from pool! Pool size: {projectilePool.Count}");
    }
}
