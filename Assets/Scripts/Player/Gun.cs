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

    private void Awake()
    {
        projectilePool = new();
    }

    private GameObject TakeFromPool()
    {
        if (projectilePool.Count < 1)
            return null;

        GameObject temp = projectilePool.Dequeue();
        Debug.Log($"Took projectile from pool! Pool size: {projectilePool.Count}");
        //temp.SetActive(true);
        return temp;
    }

    public void InitializeGun()
    {
        gunName = gunConfig.gunName;
        shootType = gunConfig.gunShootType;
        cooldownBetweenShots = gunConfig.gunCooldownBetweenShots;
        Debug.LogWarning($"Gun \"{gameObject.name}\" initialized!");
    }

    public void InitializeProjectiles()
    {
        projectile = gunConfig.gunProjectilePrefab;

        if (projectile != null)
        {
            for (int i = 0; i < projectilePoolSize; i++)
            {
                GameObject temp = Instantiate(projectile, firePoint.transform, false);
                projectilePool.Enqueue(temp);

                if (temp.TryGetComponent<Projectile>(out Projectile projectileComponent))
                {
                    projectileComponent.SetParentGun(gameObject, firePoint);
                }
                else
                {
                    Debug.LogError("No projectile components found for current projectile!");
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

        if (temp == null)
            return;

        temp.SetActive(true);
        if (temp.TryGetComponent<Projectile>(out Projectile projectileComponent))
            projectileComponent.FireProjectile();
        else
            Debug.LogError("Gun -> Shoot -> No projectile component for current projectile!");
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        projectilePool.Enqueue(objectToReturn);
        objectToReturn.SetActive(false);
        Debug.Log($"Returned projectile to pool! Pool size: {projectilePool.Count}");
    }

    // TODO: cooldown between shots coroutine
}
