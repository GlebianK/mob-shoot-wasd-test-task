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

    // НОРМАЛЬНО ОРГАНИЗОВАТЬ ПУЛ ПУЛЬ! Через словарь?.. Список?.. Массив?..

    private List<GameObject> projectilePool;

    private GameObject TakeFromPool()
    {
        Debug.Log($"Took projectile from pool! Pool size: {projectilePoolSize}");
        return null;
    }

    private void ReturnToPool(GameObject objectToReturn)
    {
        Debug.Log($"Returned projectile from pool! Pool size: { projectilePoolSize}");
    }

    public void InitializeGun()
    {
        gunName = gunConfig.gunName;
        shootType = gunConfig.gunShootType;
        cooldownBetweenShots = gunConfig.gunCooldownBetweenShots;
        projectile = gunConfig.gunProjectilePrefab;

        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject temp = Instantiate(projectile, firePoint.transform, true);
            projectilePool.Add(temp);
        }
    }

    public void Shoot()
    {
        Debug.LogWarning("БАМ, БЛ_ТЬ!");
    }
}
