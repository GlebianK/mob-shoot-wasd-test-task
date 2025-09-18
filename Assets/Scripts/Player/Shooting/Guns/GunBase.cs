using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField] protected GameObject firePoint;
    [SerializeField] protected int projectilePoolSize = 10;
    [SerializeField] protected float cooldownBetweenShots;
    [SerializeField] protected GameObject projectile;

    protected Queue<GameObject> projectilePool;
    protected bool canShoot;

    public bool IsTriggerPulled { get; set; }

    private void Awake()
    {
        projectilePool = new();
        canShoot = true;
        IsTriggerPulled = false;
    }

    protected virtual GameObject TakeFromPool()
    {
        Debug.LogWarning("This is the Base version of TakeFromPool!");
        return null;
    }

    public void InitializeProjectiles()
    {
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
            //Debug.LogWarning($"{gameObject.name}: Projectiles initialized!");
        }
        else
            Debug.LogError($"NO PROJECTILES FOR THE GUN! Gun name: {gameObject.name}");
    }

    public virtual void Shoot()
    {
        if (!canShoot)
            return;

        Debug.LogWarning("¡¿Ã, ¡À_“‹!");
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

    public void ReturnToPool(GameObject objectToReturn)
    {
        projectilePool.Enqueue(objectToReturn);
        objectToReturn.SetActive(false);
        //Debug.Log($"Returned projectile to pool! Pool size: {projectilePool.Count}");
    }

    protected IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownBetweenShots);
        canShoot = true;
    }
}
